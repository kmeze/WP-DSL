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
        public static readonly CsTag<PropertyInfo> ClassConstructorPropertyTag = "ClassConstructorProperty";
        public static readonly CsTag<PropertyInfo> DbDeltaPropertyColumnNameTag = "DbDeltaPropertyColumnName";
        public static readonly CsTag<PropertyInfo> DbDeltaPropertyColumnLengthTag = "DbDeltaPropertyColumnLength";
        public static readonly CsTag<PropertyInfo> DbDeltaPropertyColumnAttributesTag = "DbDeltaPropertyColumnAttributes";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (PropertyInfo)conceptInfo;

            string snippet = $@"{ClassConstructorPropertyTag.Evaluate(info)};

        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassConstructorTag, info.DataStructure);

            if (info.DataStructure is EntityInfo)
            {
                snippet = $@"{DbDeltaPropertyColumnNameTag.Evaluate(info)}{DbDeltaPropertyColumnLengthTag.Evaluate(info)}{DbDeltaPropertyColumnAttributesTag.Evaluate(info)}
                        ";
                codeBuilder.InsertCode(snippet, EntityCodeGenerator.DbDeltaColumnTag, (EntityInfo) info.DataStructure);
            }
        }
    }
}