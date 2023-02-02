using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DeactivationHookInfo))]
    public class DeactivationHookCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DeactivationHookInfo)conceptInfo;

            string snippet = $@"{info.WPPlugin.Name}_{info.Action.Name} ();

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DeactivationHookTag, info.WPPlugin);
        }
    }
}