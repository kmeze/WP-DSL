﻿using System;
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
    public class DataStructureCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DataStructureInfo> ClassPropertyTag = "ClassProperty";
        public static readonly CsTag<DataStructureInfo> ClassConstructorTag = "ClassConstructor";
        public static readonly CsTag<DataStructureInfo> ClassMethodTag = "ClassMethod";
        public static readonly CsTag<DataStructureInfo> DataClassParsePropertyTag = "DataClassParseProperty";
        public static readonly CsTag<DataStructureInfo> RepositoryClassPropertyTag = "RepositoryClassProperty";
        public static readonly CsTag<DataStructureInfo> RepositoryClassConstructorTag = "RepositoryClassConstructor";
        public static readonly CsTag<DataStructureInfo> RepositoryClassMethodTag = "RepositoryClassMethod";
        public static readonly CsTag<DataStructureInfo> RestControllerClassPropertyTag = "RestControllerClassProperty";
        public static readonly CsTag<DataStructureInfo> RestControllerClassConstructorTag = "RestControllerClassConstructor";
        public static readonly CsTag<DataStructureInfo> RestControllerClassRegisterRoutesTag = "RestControllerClassRegisterRoutes";
        public static readonly CsTag<DataStructureInfo> RestControllerClassMethodTag = "RestControllerClassMethod";
        public static readonly CsTag<DataStructureInfo> RestControllerAuthorizationTag = "Authorization";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;

            // Generate DataStructure class
            string snippet = $@"class {info.WPPlugin.Name}_{info.Name} {{
    {ClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        {ClassConstructorTag.Evaluate(info)}
    }}

    public static function parse( $object ):?{info.WPPlugin.Name}_{info.Name} {{
        if (! isset( $object ) ) return null;
        
        $dataStructure = new {info.WPPlugin.Name}_{info.Name}();
        {DataClassParsePropertyTag.Evaluate(info)}

        return $dataStructure;
    }}

    {ClassMethodTag.Evaluate(info)}
}}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DataStructureClassesTag, info.WPPlugin);

            // Generate DataStructure_Repository
            snippet = $@"class {info.WPPlugin.Name}_{info.Name}_Repository {{
    protected ?wpdb $wpdb = null;
    {RepositoryClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        global $wpdb;
        $this->wpdb = $wpdb;
        {RepositoryClassConstructorTag.Evaluate(info)}
    }}

    protected function parse_result( array $result ): array {{
        return array_map( fn( $row ) => {info.WPPlugin.Name}_{info.Name}::parse( $row ), $result );
    }}

    {RepositoryClassMethodTag.Evaluate(info)}
}}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DataStructureClassesTag, info.WPPlugin);

            // Generate DataStructure_REST_Controller class
            snippet = $@"class {info.WPPlugin.Name}_{info.Name}_REST_Controller {{
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

        }
    }
}