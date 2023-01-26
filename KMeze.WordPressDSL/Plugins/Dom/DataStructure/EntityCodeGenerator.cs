using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(EntityInfo))]
    public class EntityCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DataStructureInfo> ClassPropertyTag = "ClassProperty";
        public static readonly CsTag<DataStructureInfo> ColumnTag = "Column";
        public static readonly CsTag<DataStructureInfo> ColumnMapTag = "ColumnMap";
        public static readonly CsTag<DataStructureInfo> KeyMapTag = "KeyMap";
        public static readonly CsTag<DataStructureInfo> AuthorizationTag = "Authorization";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            // Generate entity class
            string snippet = $@"class {info.WPPlugin.Name}_{info.Name} {{
    public ?int $id = null;
    {ClassPropertyTag.Evaluate(info)}
}}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);

            // Generate entity repository
            snippet = $@"class {info.WPPlugin.Name}_{info.Name}_Repository {{
    public function select_{info.Name}() {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';

        return $wpdb->get_results( ""SELECT * FROM {{$table_name}};"" );
    }}

    public function select_{info.Name}_by_ID( int $id ) {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';

        return $wpdb->get_row( ""SELECT * FROM {{$table_name}} WHERE ID={{$id}};"" );
    }}

    public function insert_{info.Name}( array $data ) {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
        $wpdb->insert( $table_name, $data );

        return $wpdb->insert_id;
    }}

    public function update_{info.Name}( int $id, array $data ) {{
	    global $wpdb;
	    $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
	    $wpdb->update( $table_name, $data, array( 'id' => $id ) );
    }}

    public function delete_{info.Name}( int $id ) {{
	    global $wpdb;
	    $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
	    $wpdb->delete( $table_name, array( 'id' => $id ) );
    }}
}}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);

            // Generate entity REST controller and register routes
            snippet = $@"class {info.WPPlugin.Name}_{info.Name}_REST_Controller {{
    public function register_routes() {{
        register_rest_route( '{info.WPPlugin.Name}/v1', '/{info.Name}', array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_items' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );

        register_rest_route( '{info.WPPlugin.Name}/v1', '/{info.Name}/(?P<id>[\d]+)', array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_item' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );

        register_rest_route( '{info.WPPlugin.Name}/v1', '/{info.Name}', array(
            'methods'             => 'POST',
            'callback'            => array( $this, 'post_item' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );

        register_rest_route( '{info.WPPlugin.Name}/v1', '/{info.Name}/(?P<id>[\d]+)', array(
            'methods'             => 'PUT',
            'callback'            => array( $this, 'put_item' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );

        register_rest_route( '{info.WPPlugin.Name}/v1', '/{info.Name}/(?P<id>[\d]+)', array(
            'methods'             => 'DELETE',
            'callback'            => array( $this, 'delete_item' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );
    }}

    public function permissions_check( $request ): bool {{
        {AuthorizationTag.Evaluate(info)}

        return false;
    }}

    private function prepare_item_for_response( $row ) {{
        $entity     = new {info.WPPlugin.Name}_{info.Name}();
        $entity->id = (int) $row->ID;
        {ColumnMapTag.Evaluate(info)}

        return $entity;
    }}

    public function get_items( $request ): array {{
	    return array_map( function ( $row ) {{
	        return $this->prepare_item_for_response( $row );
	    }}, ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->select_{info.Name}() );
    }}

    public function get_item( $request ) {{
        return $this->prepare_item_for_response( ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->select_{info.Name}_by_ID( $request->get_param( 'id' ) ) );
    }}

    public function post_item( $request ) {{
        return ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->insert_{info.Name}( $request->get_json_params() );
    }}

    public function put_item( $request ) {{
        return ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->update_{info.Name}( $request->get_param( 'id' ), $request->get_json_params() );
    }}

    public function delete_item( $request ) {{
        return ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->delete_{info.Name}( $request->get_param( 'id' ) );
    }}
}}

add_action( 'rest_api_init', function () {{
    $controller = new {info.WPPlugin.Name}_{info.Name}_REST_Controller();
    $controller->register_routes();
}} );

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);

            snippet = $@"require_once( ABSPATH . 'wp-admin/includes/upgrade.php' );
    global $wpdb;
    $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
    dbDelta( ""CREATE TABLE {{$table_name}} (
                        ID BIGINT(20) NOT NULL AUTO_INCREMENT
                        {ColumnTag.Evaluate(info)}
                        ,PRIMARY KEY  (ID)
                        {KeyMapTag.Evaluate(info)}
                        ) {{$wpdb->get_charset_collate()}};"" );

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationHookTag, info.WPPlugin);
        }
    }
}