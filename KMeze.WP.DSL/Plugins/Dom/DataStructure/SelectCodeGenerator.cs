using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(SelectInfo))]
    public class SelectCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (SelectInfo)conceptInfo;

            string snippet = $@"public function get() {{
        return $this->wpdb->get_results( ""{info.Query}"" );
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassMethodTag, info);

            snippet = $@"register_rest_route( $this->namespace, $this->resource_name, array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_items' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        ";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassRegisterRoutesTag, info);

            snippet = $@"private function prepare_item_for_response( $row ) {{
        return {info.WPPlugin.Name}_{info.Name}::parse( $row );
    }}

    public function get_items( $request ): array {{
	    return array_map( function ( $row ) {{
	        return $this->prepare_item_for_response( $row );
	    }}, ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->get() );
    }}

";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassMethodTag, info);
        }
    }
}