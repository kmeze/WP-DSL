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
        public static readonly CsTag<DataStructureInfo> ClassMethodTag = "ClassMethod";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;

            string snippet = $@"class {info.WPPlugin.Name}_{info.Name} {{
    {ClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        {ClassConstructorTag.Evaluate(info)}
    }}

    {ClassMethodTag.Evaluate(info)}
}}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);
        }
    }
}