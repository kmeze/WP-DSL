using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(RoleInfo))]
    public class RoleCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<RoleInfo> CapabilityTag = "Capability";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (RoleInfo)conceptInfo;

            string snippet = $@"add_role( '{info.WPPlugin.Name}_{info.Slug}', '{info.Name}', array(
        {CapabilityTag.Evaluate(info)}
    ) );

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationHookTag, info.WPPlugin);

            snippet = $@"remove_role( '{info.WPPlugin.Name}_{info.Slug}' );

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DeactivationHookTag, info.WPPlugin);
        }
    }
}