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
    public class EntityRestControllerCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<EntityInfo> AuthorizationTag = "Authorization";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            string snippet = $@"class {info.WPPlugin.Name}_{info.Name}_REST_Controller {{
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
        return {info.WPPlugin.Name}_{info.Name}::parse( $row );
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
        }
    }
}