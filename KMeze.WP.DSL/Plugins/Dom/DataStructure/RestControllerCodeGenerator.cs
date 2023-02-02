using System;
using System.ComponentModel.Composition;
using Newtonsoft.Json.Linq;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DataStructureInfo))]
    public class RestControllerCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DataStructureInfo> RestControllerClassPropertyTag = "RestControllerClassProperty";
        public static readonly CsTag<DataStructureInfo> RestControllerClassConstructorTag = "RestControllerClassConstructor";
        public static readonly CsTag<DataStructureInfo> RestControllerClassRegisterRoutesTag = "RestControllerClassRegisterRoutes";
        public static readonly CsTag<DataStructureInfo> RestControllerClassMethodTag = "RestControllerClassMethod";
        public static readonly CsTag<DataStructureInfo> RestControllerAuthorizationTag = "Authorization";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;

            string snippet = $@"class {info.WPPlugin.Name}_{info.Name}_REST_Controller {{
    protected ?string $namespace = null;
    protected ?string $resource_name = null;
    {RestControllerClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        $this->namespace     = '/{info.WPPlugin.Name}/v1';
        $this->resource_name = '/{info.Name}';
        {RestControllerClassConstructorTag.Evaluate(info)}
    }}

    public function register_routes() {{
        {RestControllerClassRegisterRoutesTag.Evaluate(info)}
    }}

    public function item_permissions_check( $request ): bool {{
        {RestControllerAuthorizationTag.Evaluate(info)}

        return false;
    }}

    {RestControllerClassMethodTag.Evaluate(info)}
}}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DataStructureClassesTag, info.WPPlugin);

            snippet = $@"add_action( 'rest_api_init', function () {{
    ( new {info.WPPlugin.Name}_{info.Name}_REST_Controller() )->register_routes();
}} );
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActionHooksTag, info.WPPlugin);
        }
    }
}