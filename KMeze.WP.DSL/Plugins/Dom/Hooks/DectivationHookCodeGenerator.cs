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

            string snippet = $@"{info.Plugin.Slug}_{info.Callback.Name} ();

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DeactivationHookTag, info.Plugin);
        }
    }
}