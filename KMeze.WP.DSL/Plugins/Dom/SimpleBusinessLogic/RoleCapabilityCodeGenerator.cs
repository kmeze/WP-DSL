using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(RoleCapabilityInfo))]
    public class CapabilityCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (RoleCapabilityInfo)conceptInfo;

            string snippet = $@"'{info.Role.Plugin.Name}_{info.Capability.Slug}' => true, ";
            codeBuilder.InsertCode(snippet, RoleCodeGenerator.CapabilityTag, info.Role);
        }
    }
}