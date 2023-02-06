using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(PropertyInfo))]
    public class PropertyCodeGenerator : IWPPluginConceptCodeGenerator
    {
        // TODO: fix tag string (remove Tax sufix)
        public static readonly CsTag<PropertyInfo> ClassConstructorPropertyTag = "ClassConstructorPropertyTag";
        public static readonly CsTag<PropertyInfo> DbDeltaPropertyColumnTag = "DbDeltaPropertyColumn";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (PropertyInfo)conceptInfo;

            string snippet = $@"{ClassConstructorPropertyTag.Evaluate(info)};

        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassConstructorTag, info.DataStructure);

            if (info.DataStructure is EntityInfo)
            {
                snippet = $@"{DbDeltaPropertyColumnTag.Evaluate(info)} 
                        ";
                codeBuilder.InsertCode(snippet, EntityDbDeltaCodeGenerator.DbDeltaColumnTag, info.DataStructure);
            }
        }
    }
}