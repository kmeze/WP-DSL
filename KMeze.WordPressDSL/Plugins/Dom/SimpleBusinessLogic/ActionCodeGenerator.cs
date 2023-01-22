using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ActionInfo))]
    public class ActionCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ActionInfo)conceptInfo;

            string snippet = $@"function {info.WPPlugin.Name}_{info.Name}() {{
    {info.Script}
}}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);
        }
    }
}