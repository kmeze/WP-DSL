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
    public class DataStructureCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DataStructureInfo> DataStructureClassPropertyTag = "DataStructureClassProperty";
        public static readonly CsTag<DataStructureInfo> DataClassConstructorTag = "DataClassConstructor";
        public static readonly CsTag<DataStructureInfo> DataClassMethodTag = "DataClassMethod";
        public static readonly CsTag<DataStructureInfo> DataClassParsePropertyTag = "DataClassParseProperty";
        public static readonly CsTag<DataStructureInfo> RepositoryClassPropertyTag = "RepositoryClassProperty";
        public static readonly CsTag<DataStructureInfo> RepositoryClassConstructorTag = "RepositoryClassConstructor";
        public static readonly CsTag<DataStructureInfo> RepositoryClassMethodTag = "RepositoryClassMethod";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;

            // Generate DataStructure class
            string snippet = $@"class {info.WPPlugin.Name}_{info.Name} {{
    {DataStructureClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        {DataClassConstructorTag.Evaluate(info)}
    }}

    public static function parse( $object ):?{info.WPPlugin.Name}_{info.Name} {{
        if (! isset( $object ) ) return null;
        
        $dataStructure = new {info.WPPlugin.Name}_{info.Name}();
        {DataClassParsePropertyTag.Evaluate(info)}

        return $dataStructure;
    }}

    {DataClassMethodTag.Evaluate(info)}
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
        }
    }
}