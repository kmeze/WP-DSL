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

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;

            string snippet = $@"class {info.WPPlugin.Name}_{info.Name} {{
    {DataStructureClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        {DataClassConstructorTag.Evaluate(info)}
    }}

    public static function parse( $object ):{info.WPPlugin.Name}_{info.Name} {{
        $dataStructure = new {info.WPPlugin.Name}_{info.Name}();
        {DataClassParsePropertyTag.Evaluate(info)}

        return $dataStructure;
    }}

    {DataClassMethodTag.Evaluate(info)}
}}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);
        }
    }
}