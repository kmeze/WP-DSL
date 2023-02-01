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
        public static readonly CsTag<ListInfo> AuthorizationTag = "Authorization";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ListInfo)conceptInfo;

            string snippet = $@"public function get() {{

        // TODO: IMPORTANT CVIS_ MUST BE wpdb->prefilx; MULTIPLE CS FILES
        $sql = ""SELECT cvis_{info.Source.WPPlugin.Name}_{info.Source.Name}.ID
                    {ListColumnTag.Evaluate(info)}
                FROM cvis_{info.Source.WPPlugin.Name}_{info.Source.Name}
                {ListJoinTag.Evaluate(info)};"";

        return $this->wpdb->get_results( $sql );
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassMethodTag, info);

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

        ";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassRegisterRoutesTag, info);

            snippet = $@"public function item_permissions_check( $request ): bool {{
        {AuthorizationTag.Evaluate(info)}

        return false;
    }}

    private function prepare_item_for_response( $row ) {{
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