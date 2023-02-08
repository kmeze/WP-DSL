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
    [ExportMetadata(MefProvider.Implements, typeof(RepositoryDataStructureInfo))]
    public class RepositoryDataStructureCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DataStructureInfo> RepositoryClassPropertyTag = "RepositoryClassProperty";
        public static readonly CsTag<DataStructureInfo> RepositoryClassConstructorTag = "RepositoryClassConstructor";
        public static readonly CsTag<DataStructureInfo> RepositoryClassMethodTag = "RepositoryClassMethod";
        public static readonly CsTag<DataStructureInfo> RestControllerClassPropertyTag = "RestControllerClassProperty";
        public static readonly CsTag<DataStructureInfo> RestControllerClassConstructorTag = "RestControllerClassConstructor";
        public static readonly CsTag<DataStructureInfo> RestControllerClassRegisterRoutesTag = "RestControllerClassRegisterRoutes";
        public static readonly CsTag<DataStructureInfo> RestControllerClassParamToConditionTag = "RestControllerClassParamToCondition";
        public static readonly CsTag<DataStructureInfo> RestControllerClassMethodTag = "RestControllerClassMethod";
        public static readonly CsTag<DataStructureInfo> RestControllerAuthorizationTag = "Authorization";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;

            // Generate DataStructure_Repository
            string snippet = $@"class {info.Plugin.Name}_{info.Name}_Repository {{
    protected ?wpdb $wpdb = null;
    {RepositoryClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        global $wpdb;
        $this->wpdb = $wpdb;
        {RepositoryClassConstructorTag.Evaluate(info)}
    }}

    protected function parse_result( array $result ): array {{
        return array_map( fn( $row ) => {info.Plugin.Name}_{info.Name}::parse( $row ), $result );
    }}

    protected function transform_conditions($conditions) {{
        $name_value = array();
        $formats    = array();
        $segments   = array();
        $args             = array();
        foreach ( $conditions as $condition ) {{
            $name   = $condition['Name'];
            $value  = $condition['Value'];
            $format = $condition['Format'];

            // For update & delete
            $name_value[ $name ] = $value;
            $formats[]           = $format;

            // For prepare method
	        if ($value === 'null') {{
		        $segments[] = ""($name IS NULL)"";
	        }} else {{
		        $segments[] = ""($name=$format)"";
		        $args[]           = $value;
	        }}
        }}

        return array(
            'NAME_VALUE' => $name_value,
            'FORMATS' => $formats,
            'SEGMENTS' => $segments,
            'ARGS' => $args,
        );
    }}

    {RepositoryClassMethodTag.Evaluate(info)}
}}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DataStructureClassesTag, info.Plugin);

            // Generate DataStructure_REST_Controller class
            snippet = $@"class {info.Plugin.Name}_{info.Name}_REST_Controller {{
    protected ?string $namespace = null;
    protected ?string $resource_name = null;
    {RestControllerClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        $this->namespace     = '/{info.Plugin.Name}/v1';
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

    protected function request_parameters_to_condition( $params = null ): array {{
		$conditions = [];

        if( $params !==  null ) {{
            {RestControllerClassParamToConditionTag.Evaluate(info)}
        }}

		return $conditions;
	}}

    {RestControllerClassMethodTag.Evaluate(info)}
}}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DataStructureClassesTag, info.Plugin);

        }
    }
}