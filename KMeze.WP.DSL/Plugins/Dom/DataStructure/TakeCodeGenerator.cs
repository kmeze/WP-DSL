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

            string snippet = $@",$this->source_table_name.{info.Property.Name} AS {info.Property.Name}
                    ";
            codeBuilder.InsertCode(snippet, ListCodeGenerator.ListColumnTag, info.List);
        }
    }
}