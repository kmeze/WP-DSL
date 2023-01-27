using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(WPRoleCapabilityInfo))]
    public class WPRoleCapabilityCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (WPRoleCapabilityInfo)conceptInfo;

            string snippet = $@"    $role = get_role( '{info.Role.Slug.Trim().ToLower()}' );
    if ( isset( $role ) ) $role->add_cap( '{info.Role.WPPlugin.Name}_{info.Capability.Slug}', true );

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationHookTag, info.Role.WPPlugin);

            snippet = $@"    $role = get_role( '{info.Role.Slug.Trim().ToLower()}' );
    if ( isset( $role ) ) $role->remove_cap( '{info.Role.WPPlugin.Name}_{info.Capability.Slug}' );

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DeactivationHookTag, info.Role.WPPlugin);
        }
    }
}