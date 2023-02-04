using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(EntityInfo))]
    public class EntityRepositoryCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            string snippet = $@"protected ?string ${info.Name}_table_name = null;
    ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.RepositoryClassPropertyTag, info);

            snippet = $@"$this->{info.Name}_table_name = $this->wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.RepositoryClassConstructorTag, info);

            snippet = $@"public function transform_conditions($conditions) {{
        $name_value = array();
        $formats    = array();
        $segments   = array();
        $args             = array();
        foreach ( $conditions as $condition ) {{
            $name   = $condition['Name'];
            $value  = $condition['Value'];
            $format = $condition['Format'];

            // For update & delete
            $name_value[ $name ] = $value;
            $formats[]           = $format;

            // For prepare method
            $segments[] = ""($name=$format)"";
            $args[]           = $value;
        }}

        return array(
            'NAME_VALUE' => $name_value,
            'FORMATS' => $formats,
            'SEGMENTS' => $segments,
            'ARGS' => $args,
        );
    }}

    public function get() {{
        $conditions = [];
        $conditions = apply_filters( '{info.WPPlugin.Name}_{info.Name}_filter', $conditions );
        $transformed = $this->transform_conditions($conditions);
        $where_part = ! empty( $transformed['SEGMENTS'] ) ? 'AND ' . implode( ' AND ', $transformed['SEGMENTS'] ) : '';
        $sql = $this->wpdb->prepare( ""SELECT * FROM $this->{info.Name}_table_name WHERE (1=1) {{$where_part}};"", $transformed['ARGS'] );

        return $this->parse_result( $this->wpdb->get_results( $sql ) );
    }}

    public function get_by_ID( int $id ) {{
        $conditions = [];
        $conditions = apply_filters( '{info.WPPlugin.Name}_{info.Name}_filter', $conditions );
        $conditions[] = array( 'Name' => 'ID', 'Value' => $id, 'Format' => '%d' );
        $transformed = $this->transform_conditions($conditions);
        $where_part = ! empty( $transformed['SEGMENTS'] ) ? 'AND ' . implode( ' AND ', $transformed['SEGMENTS'] ) : '';
        $sql = $this->wpdb->prepare( ""SELECT * FROM $this->{info.Name}_table_name WHERE (1=1) {{$where_part}};"", $transformed['ARGS'] );
        $row = $this->wpdb->get_row( $sql );

        return {info.WPPlugin.Name}_{info.Name}::parse( $row );
    }}

    public function insert( array $data ): int {{
        $this->wpdb->insert( $this->{info.Name}_table_name, $data );

        return $this->insert_id;
    }}

    public function update( int $id, array $data ) {{
	    $this->wpdb->update( $this->{info.Name}_table_name, $data, array( 'ID' => $id ) );
    }}

    public function delete( int $id ) {{
	    $this->wpdb->delete( $this->{info.Name}_table_name, array( 'ID' => $id ) );
    }}

";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.RepositoryClassMethodTag, info);
        }
    }
}