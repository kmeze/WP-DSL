using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DataStructureActionInfo))]
    public class DataStructureActionCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureActionInfo)conceptInfo;

            string snippet = $@"register_rest_route( $this->namespace, $this->resource_name . '/{info.Action.Name}', array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_{info.Action.Name}' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        ";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassRegisterRoutesTag, info.DataStructure);

            snippet = $@"   public function get_{info.Action.Name}( $request ) {{
        return {info.Action.WPPlugin.Name}_{info.Action.Name}( $request );
    }}

    ";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassMethodTag, info.DataStructure);

            snippet = $@" $request ";
            codeBuilder.InsertCode(snippet, CallbackCodeGenerator.CallbackArgTag, info.Action);
        }
    }
}