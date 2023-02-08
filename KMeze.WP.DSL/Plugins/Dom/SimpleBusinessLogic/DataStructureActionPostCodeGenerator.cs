using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DataStructureActionPostInfo))]
    public class DataStructureActionPostCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureActionPostInfo)conceptInfo;

            string snippet = $@"register_rest_route( $this->namespace, $this->resource_name . '/{info.Callback.Name}', array(
            'methods'             => 'POST',
            'callback'            => array( $this, 'post_{info.Callback.Name}' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassRegisterRoutesTag, info.DataStructure);

            snippet = $@"   public function post_{info.Callback.Name}( $request ) {{
        return {info.Callback.Plugin.Name}_{info.Callback.Name}( $request );
    }}

    ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassMethodTag, info.DataStructure);

            snippet = $@" $request ";
            codeBuilder.InsertCode(snippet, CallbackCodeGenerator.CallbackArgTag, info.Callback);
        }
    }
}