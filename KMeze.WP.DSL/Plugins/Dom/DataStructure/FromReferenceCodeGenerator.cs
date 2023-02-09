using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(FromReferenceMacroInfo))]
    public class FromReferenceCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (FromReferenceMacroInfo)conceptInfo;

            string snippet = $@"protected ?string $join_{info.SourceReferencePropertyInfo.Name}_table_name = null;
    ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RepositoryClassPropertyTag, info.List);

            snippet = $@"$this->join_{info.SourceReferencePropertyInfo.Name}_table_name = $this->wpdb->prefix . '{info.SourceReferencePropertyInfo.ReferencedDataStructure.Plugin.Slug}_{info.SourceReferencePropertyInfo.ReferencedDataStructure.Name}';
        ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RepositoryClassConstructorTag, info.List);


            // TODO: IMPORTANT CVIS_ MUST BE wpdb->prefilx
            snippet = $@"LEFT OUTER JOIN $this->join_{info.SourceReferencePropertyInfo.Name}_table_name ON $this->source_table_name.{info.SourceReferencePropertyInfo.Name}_id = $this->join_{info.SourceReferencePropertyInfo.Name}_table_name.ID
";
            codeBuilder.InsertCode(snippet, ListCodeGenerator.ListJoinTag, info.List);
        }
    }
}