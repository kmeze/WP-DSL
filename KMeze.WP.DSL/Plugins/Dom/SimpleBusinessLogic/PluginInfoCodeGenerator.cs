using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(PluginInfoInfo))]
    public class PluginInfoCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (PluginInfoInfo)conceptInfo;

            string snippet = @$" * {info.Key}: {info.Value}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PluginHeaderTag, info.Plugin);
        }
    }
}