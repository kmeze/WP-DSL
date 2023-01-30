using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(FromReferenceInfo))]
    public class FromReferenceCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (FromReferenceInfo)conceptInfo;

            // TODO: IMPORTANT CVIS_ MUST BE wpdb->prefilx
            string snippet = $@"LEFT OUTER JOIN cvis_{info.Reference.Referenced.WPPlugin.Name}_{info.Reference.Referenced.Name} ON cvis_{info.Reference.DataStructure.WPPlugin.Name}_{info.Reference.DataStructure.Name}.{info.Reference.Name}_id = cvis_{info.Reference.Referenced.WPPlugin.Name}_{info.Reference.Referenced.Name}.ID
";
            codeBuilder.InsertCode(snippet, ListCodeGenerator.ListJoinTag, info.List);
        }
    }
}