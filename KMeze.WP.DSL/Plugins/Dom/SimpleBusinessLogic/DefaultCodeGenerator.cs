using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DefaultInfo))]
    public class DefaultCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DefaultInfo> ClassConstructorPropertyTag = "ClassConstructorPropertyTag";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DefaultInfo)conceptInfo;

            var properyName = info.Property.Name;
            if (info.Property is ReferencePropertyInfo) properyName = $@"{info.Property.Name}_id";

            string snippet = $@"${info.Property.Name}DefaultValue = fn () => {info.Expression};
        $this->{properyName} = ${info.Property.Name}DefaultValue();
        ";
            codeBuilder.InsertCode(snippet, PropertyCodeGenerator.ClassConstructorPropertyTag, info.Property);
        }
    }
}