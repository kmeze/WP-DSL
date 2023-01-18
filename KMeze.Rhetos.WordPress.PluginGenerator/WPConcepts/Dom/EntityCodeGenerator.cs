using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(EntityInfo))]
    public class EntityCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<EntityInfo> PropertyTag = "Property";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            string snippet = $@"class {info.Name} {{
    public ?int $id = null;
    {PropertyTag.Evaluate(info)}
}}

class {info.Name}_REST_Controller {{
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
        return true;
    }}

    public function get_items( $request ) {{
	    return new WP_Error( 'rest_endpoint_not_implemented', esc_html__( 'Endpoint is not implemented.', '{info.WPPlugin.Name}' ), array( 'status' => '500' ) );
    }}

    public function get_item( $request ) {{
        return new WP_Error( 'rest_endpoint_not_implemented', esc_html__( 'Endpoint is not implemented.', '{info.WPPlugin.Name}' ), array( 'status' => '500' ) );
    }}

    public function post_item( $request ) {{
        return new WP_Error( 'rest_endpoint_not_implemented', esc_html__( 'Endpoint is not implemented.', '{info.WPPlugin.Name}' ), array( 'status' => '500' ) );
    }}

    public function put_item( $request ) {{
        return new WP_Error( 'rest_endpoint_not_implemented', esc_html__( 'Endpoint is not implemented.', '{info.WPPlugin.Name}' ), array( 'status' => '500' ) );
    }}

    public function delete_item( $request ) {{
        return new WP_Error( 'rest_endpoint_not_implemented', esc_html__( 'Endpoint is not implemented.', '{info.WPPlugin.Name}' ), array( 'status' => '500' ) );
    }}
}}

add_action( 'rest_api_init', function () {{
    $controller = new {info.Name}_REST_Controller();
    $controller->register_routes();
}} );

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);
        }
    }
}