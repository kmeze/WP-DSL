using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(UniqueMultipleInfo))]
    public class UniqueMultipleCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (UniqueMultipleInfo)conceptInfo;

            string snippet = $@",UNIQUE key_{info.Entity.WPPlugin.Name}_{info.Entity.Name}_{info.Columns.Trim().Replace(" ", "").Replace(",", "_")} ({info.Columns.Trim()})
                        ";
            codeBuilder.InsertCode(snippet, EntityDbDeltaCodeGenerator.DbDeltaKeyTag, info.Entity);
        }
    }
}