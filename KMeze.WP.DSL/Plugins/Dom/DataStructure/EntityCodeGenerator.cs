using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(EntityInfo))]
    public class EntityCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<EntityInfo> DbDeltaColumnTag = "DbDeltaColumn";
        public static readonly CsTag<EntityInfo> DbDeltaKeyTag = "DbDeltaKey";
        public static readonly CsTag<EntityInfo> DbDeltaAfterTag = "DbDeltaAfter";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            string snippet = $@"public ?int $id = null;
    ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassPropertyTag, info);

            snippet = $@"$dataStructure->id = (int) $object->ID;
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassParsePropertyTag, info);

            snippet = $@"protected ?string ${info.Name}_table_name = null;
    ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RepositoryClassPropertyTag, info);

            snippet = $@"$this->{info.Name}_table_name = $this->wpdb->prefix . '{info.Plugin.Slug}_{info.Name}';
        ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RepositoryClassConstructorTag, info);

            snippet = $@"public function get($conditions = []) {{
        $conditions = apply_filters( '{info.Plugin.Slug}_{info.Name}_filter', $conditions );
        $transformed = $this->transform_conditions($conditions);
        $where_part = ! empty( $transformed['SEGMENTS'] ) ? 'AND ' . implode( ' AND ', $transformed['SEGMENTS'] ) : '';
        $sql = $this->wpdb->prepare( ""SELECT * FROM $this->{info.Name}_table_name WHERE (1=1) {{$where_part}};"", $transformed['ARGS'] );

        return $this->parse_result( $this->wpdb->get_results( $sql ) );
    }}

    public function get_by_ID( int $id ) {{
        $conditions = [];
        $conditions = apply_filters( '{info.Plugin.Slug}_{info.Name}_filter', $conditions );
        $conditions[] = array( 'Name' => 'ID', 'Value' => $id, 'Format' => '%d' );
        $transformed = $this->transform_conditions($conditions);
        $where_part = ! empty( $transformed['SEGMENTS'] ) ? 'AND ' . implode( ' AND ', $transformed['SEGMENTS'] ) : '';
        $sql = $this->wpdb->prepare( ""SELECT * FROM $this->{info.Name}_table_name WHERE (1=1) {{$where_part}};"", $transformed['ARGS'] );
        $row = $this->wpdb->get_row( $sql );

        return {info.Plugin.Slug}_{info.Name}::parse( $row );
    }}

    public function insert( array $data ): ?int {{
        $data = apply_filters( '{info.Plugin.Slug}_{info.Name}_insert', $data );
        $this->wpdb->insert( $this->{info.Name}_table_name, $data );

        return $this->wpdb->insert_id;
    }}

    public function update( int $id, array $data ) {{
        $conditions = [];
        $conditions = apply_filters( '{info.Plugin.Slug}_{info.Name}_filter', $conditions );
        $conditions[] = array( 'Name' => 'ID', 'Value' => $id, 'Format' => '%d' );
        $transformed = $this->transform_conditions($conditions);
        $data = apply_filters( '{info.Plugin.Slug}_{info.Name}_update', $data );

	    return $this->wpdb->update( $this->{info.Name}_table_name, $data, $transformed['NAME_VALUE'], null, $transformed['ARGS']);
    }}

    public function delete( int $id ) {{
        $conditions = [];
        $conditions = apply_filters( '{info.Plugin.Slug}_{info.Name}_filter', $conditions );
        $conditions[] = array( 'Name' => 'ID', 'Value' => $id, 'Format' => '%d' );
        $transformed = $this->transform_conditions($conditions);

	    return $this->wpdb->delete( $this->{info.Name}_table_name, $transformed['NAME_VALUE'], $transformed['ARGS'] );
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RepositoryClassMethodTag, info);

            snippet = $@"register_rest_route( $this->namespace, $this->resource_name, array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_items' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        register_rest_route( $this->namespace, $this->resource_name . '/(?P<id>[\d]+)', array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_item' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        register_rest_route( $this->namespace, $this->resource_name, array(
            'methods'             => 'POST',
            'callback'            => array( $this, 'create_item' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        register_rest_route( $this->namespace, $this->resource_name . '/(?P<id>[\d]+)', array(
            'methods'             => 'PUT',
            'callback'            => array( $this, 'update_item' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        register_rest_route( $this->namespace, $this->resource_name . '/(?P<id>[\d]+)', array(
            'methods'             => 'DELETE',
            'callback'            => array( $this, 'delete_item' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );
        ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassRegisterRoutesTag, info);

            snippet = $@"public function get_items( $request ): array {{
        $parameters = $request->get_params();
		$conditions = $this->request_parameters_to_condition( $parameters );

        return ( new {info.Plugin.Slug}_{info.Name}_Repository() )->get($conditions);
    }}

    public function get_item( $request ) {{
        return ( new {info.Plugin.Slug}_{info.Name}_Repository() )->get_by_ID( $request->get_param( 'id' ) );
    }}

    public function create_item( $request ) {{
        $repository = new {info.Plugin.Slug}_{info.Name}_Repository();
        $insert_id  = $repository->insert( $request->get_json_params() );

        return $repository->get_by_ID( $insert_id );
    }}

    public function update_item( $request ) {{
        return ( new {info.Plugin.Slug}_{info.Name}_Repository() )->update( $request->get_param( 'id' ), $request->get_json_params() );
    }}

    public function delete_item( $request ) {{
        return ( new {info.Plugin.Slug}_{info.Name}_Repository() )->delete( $request->get_param( 'id' ) );
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassMethodTag, info);

            snippet = $@"require_once( ABSPATH . 'wp-admin/includes/upgrade.php' );
    global $wpdb;
    $table_name = $wpdb->prefix . '{info.Plugin.Slug}_{info.Name}';
    dbDelta( ""CREATE TABLE {{$table_name}} (
                        ID BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT
                        {DbDeltaColumnTag.Evaluate(info)}
                        ,PRIMARY KEY  (ID)
                        {DbDeltaKeyTag.Evaluate(info)}
                        ) {{$wpdb->get_charset_collate()}};"" );

    {DbDeltaAfterTag.Evaluate(info)}

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationDbDeltaHookTag, info.Plugin);

        }
    }
}