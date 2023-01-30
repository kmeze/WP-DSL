using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(TakeFromReferenceInfo))]
    public class TakeFromReferenceCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (TakeFromReferenceInfo)conceptInfo;

            // TODO: IMPORTANT CVIS_ MUST BE wpdb->prefilx; MULTIPLE CS FILES
            string snippet = $@", cvis_{info.Reference.Reference.Referenced.WPPlugin.Name}_{info.Reference.Reference.Referenced.Name}.{info.Property.Name}
    ";
            codeBuilder.InsertCode(snippet, ListCodeGenerator.ListColumnTag, info.Reference.List);
        }
    }
}