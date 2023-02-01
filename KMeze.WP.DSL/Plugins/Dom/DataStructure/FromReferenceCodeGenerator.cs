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
            string snippet = $@"LEFT OUTER JOIN cvis_{info.SourceReferencePropertyName.ReferencedDataStructure.WPPlugin.Name}_{info.SourceReferencePropertyName.ReferencedDataStructure.Name} ON cvis_{info.SourceReferencePropertyName.DataStructure.WPPlugin.Name}_{info.SourceReferencePropertyName.DataStructure.Name}.{info.SourceReferencePropertyName.Name}_id = cvis_{info.SourceReferencePropertyName.ReferencedDataStructure.WPPlugin.Name}_{info.SourceReferencePropertyName.ReferencedDataStructure.Name}.ID
";
            codeBuilder.InsertCode(snippet, ListCodeGenerator.ListJoinTag, info.List);
        }
    }
}