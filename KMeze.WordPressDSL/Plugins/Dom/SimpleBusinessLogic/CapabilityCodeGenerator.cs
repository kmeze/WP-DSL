using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(CapabilityInfo))]
    public class CapabilityCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (CapabilityInfo)conceptInfo;

            string snippet = $@"'{info.Cap}', ";
            codeBuilder.InsertCode(snippet, RoleCodeGenerator.CapTag, info.Role);
        }
    }
}