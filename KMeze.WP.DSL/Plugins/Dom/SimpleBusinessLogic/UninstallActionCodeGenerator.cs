using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(UninstallActionInfo))]
    public class UninstallActionCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (UninstallActionInfo)conceptInfo;

            string snippet = $@"{info.WPPlugin.Name}_{info.Action.Name} ();

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.UninstallHookTag, info.WPPlugin);
        }
    }
}