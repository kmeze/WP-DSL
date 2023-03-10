using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ListInfo))]
    public class ListCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<ListInfo> ListColumnTag = "ListColumn";
        public static readonly CsTag<ListInfo> ListJoinTag = "ListJoin";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ListInfo)conceptInfo;

            string snippet = $@"public ?int $id = null;
    ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassPropertyTag, info);

            snippet = $@"$dataStructure->id = (int) $object->ID;
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassParsePropertyTag, info);

            snippet = $@"protected ?string $source_table_name = null;
    ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RepositoryClassPropertyTag, info);

            snippet = $@"$this->source_table_name = $this->wpdb->prefix . '{info.Source.Plugin.Slug}_{info.Source.Name}';
        ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RepositoryClassConstructorTag, info);

            snippet = $@"public function get($conditions = []) {{
                $conditions = apply_filters( '{info.Plugin.Slug}_{info.Name}_filter', $conditions );
                $transformed = $this->transform_conditions($conditions);
                $where_part = ! empty( $transformed['SEGMENTS'] ) ? 'AND ' . implode( ' AND ', $transformed['SEGMENTS'] ) : '';
                $sql = $this->wpdb->prepare( ""SELECT $this->source_table_name.ID AS ID
                            {ListColumnTag.Evaluate(info)}
                            FROM $this->source_table_name
                            {ListJoinTag.Evaluate(info)}
                            WHERE (1=1) {{$where_part}};"", $transformed['ARGS'] );

        return $this->parse_result( $this->wpdb->get_results( $sql ) );
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RepositoryClassMethodTag, info);

            snippet = $@"register_rest_route( $this->namespace, $this->resource_name, array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_items' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassRegisterRoutesTag, info);

            snippet = $@"public function get_items( $request ) {{
        $parameters = $request->get_params();
		$conditions = $this->request_parameters_to_condition( $parameters );

        return ( new {info.Plugin.Slug}_{info.Name}_Repository() )->get($conditions);
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassMethodTag, info);
        }
    }
}