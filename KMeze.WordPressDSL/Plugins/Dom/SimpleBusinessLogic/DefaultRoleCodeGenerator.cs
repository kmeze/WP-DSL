using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DefaultRoleInfo))]
    public class DefaultRoleCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DefaultRoleInfo)conceptInfo;

            string snippet = $@"update_option(' default_role', '{info.WPPlugin.Name}_{info.Role.Slug}' );

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationHookTag, info.WPPlugin);

            snippet = $@"update_option( 'default_role', 'subscriber' );

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DeactivationHookTag, info.WPPlugin);
        }
    }
}