using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ActivationHookInfo))]
    public class ActivationHookCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ActivationHookInfo)conceptInfo;

            string snippet = $@"{info.WPPlugin.Name}_{info.Action.Name} ();

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationHookTag, info.WPPlugin);
        }
    }
}