using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(RoleInfo))]
    public class RoleCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<RoleInfo> CapabilityTag = "Capability";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (RoleInfo)conceptInfo;

            string snippet = $@"add_role( '{info.Plugin.Name}_{info.Slug}', '{info.Name}', array(
        {CapabilityTag.Evaluate(info)}
    ) );

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationHookTag, info.Plugin);

            snippet = $@"remove_role( '{info.Plugin.Name}_{info.Slug}' );

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DeactivationHookTag, info.Plugin);
        }
    }
}