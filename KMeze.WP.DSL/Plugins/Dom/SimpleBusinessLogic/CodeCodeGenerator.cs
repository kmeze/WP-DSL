using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(CodeInfo))]
    public class CodeConceptGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (CodeInfo)conceptInfo;

            string snippet = $@"{info.Script}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.CodeTag, info.WPPlugin);
        }
    }
}