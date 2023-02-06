using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(UniqueInfo))]
    public class UniqueCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (UniqueInfo)conceptInfo;

            string snippet = $@",UNIQUE key_{info.Property.DataStructure.Name}_{info.Property.Name} ({info.Property.Name})
                        ";
            codeBuilder.InsertCode(snippet, EntityDbDeltaCodeGenerator.DbDeltaKeyTag, info.Property.DataStructure);
        }
    }
}