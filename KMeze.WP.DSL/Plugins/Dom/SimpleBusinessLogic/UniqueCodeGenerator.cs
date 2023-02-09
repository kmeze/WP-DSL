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

            // Fast hack for to enable Unique key for reference (ignores NULL)
            string keyName = $@"ukey_{info.Property.DataStructure.Plugin.Slug}_{info.Property.DataStructure.Name}_{info.Property.Name}"; 
            string column = $@"{info.Property.Name}";
            if (info.Property is ReferencePropertyInfo)
            {
                keyName = $@"{keyName}_id";
                column = $@"{column}_id";
            }

            string snippet = $@",UNIQUE {keyName} ({column})
                        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.DbDeltaKeyTag, (EntityInfo) info.Property.DataStructure);
        }
    }
}