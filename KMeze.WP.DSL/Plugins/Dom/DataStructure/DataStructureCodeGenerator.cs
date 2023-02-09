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
        public static readonly CsTag<DataStructureInfo> ClassPropertyTag = "ClassProperty";
        public static readonly CsTag<DataStructureInfo> ClassConstructorTag = "ClassConstructor";
        public static readonly CsTag<DataStructureInfo> ClassParsePropertyTag = "ClassParseProperty";
        public static readonly CsTag<DataStructureInfo> ClassMethodTag = "ClassMethod";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;

            // Generate DataStructure class
            string snippet = $@"class {info.Plugin.Slug}_{info.Name} {{
    {ClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        {ClassConstructorTag.Evaluate(info)}
    }}

    public static function parse( object $object ):?{info.Plugin.Slug}_{info.Name} {{
        if (! isset( $object ) ) return null;
        
        $dataStructure = new {info.Plugin.Slug}_{info.Name}();
        {ClassParsePropertyTag.Evaluate(info)}

        return $dataStructure;
    }}

    {ClassMethodTag.Evaluate(info)}
}}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DataStructureClassesTag, info.Plugin);
        }
    }
}