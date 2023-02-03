using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(UninstallHookInfo))]
    public class UninstallHookCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (UninstallHookInfo)conceptInfo;

            string snippet = $@"{info.WPPlugin.Name}_{info.Action.Name} ();

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.UninstallHookTag, info.WPPlugin);
        }
    }
}