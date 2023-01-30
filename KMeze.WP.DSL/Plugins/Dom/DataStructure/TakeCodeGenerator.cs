using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(TakeInfo))]
    public class TakeCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (TakeInfo)conceptInfo;

            // TODO: IMPORTANT CVIS_ MUST BE wpdb->prefilx; MULTIPLE CS FILES
            string snippet = $@", cvis_{info.Property.DataStructure.WPPlugin.Name}_{info.Property.DataStructure.Name}.{info.Property.Name}
    ";
            codeBuilder.InsertCode(snippet, ListCodeGenerator.ListColumnTag, info.List);
        }
    }
}