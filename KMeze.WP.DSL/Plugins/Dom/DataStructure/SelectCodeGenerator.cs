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
	    $table = fn (string $entityFullName):string => $this->wpdb->prefix . str_replace('.', '_', $entityFullName);
        $user_id = fn ():int => get_current_user_id();

        return $this->parse_result( $this->wpdb->get_results( ""{info.Query}"" ) );
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

            snippet = $@"public function get_items( $request ): array {{
	    return ( new {info.Plugin.Slug}_{info.Name}_Repository() )->get();
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassMethodTag, info);
        }
    }
}