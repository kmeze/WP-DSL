using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ShortStringPropertyInfo))]
    public class ShortStringPropertyCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ShortStringPropertyInfo)conceptInfo;

            string snippet = $@"public ?string ${info.Name} = null;
    ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.PropertyTag, info.Entity);
        }
    }
}