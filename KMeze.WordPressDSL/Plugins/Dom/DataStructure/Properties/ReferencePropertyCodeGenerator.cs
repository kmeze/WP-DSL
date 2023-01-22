using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ReferencePropertyInfo))]
    public class ReferencePropertyCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ReferencePropertyInfo)conceptInfo;

            string snippet = $@"public ?int ${info.Name}_id = null;
    ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ClassPropertyTag, info.Entity);

            snippet = $@",{info.Name}_id BIGINT(20) DEFAULT NULL
                        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ColumnTag, info.Entity);

            snippet = $@"$entity->{info.Name}_id = is_null($row->{info.Name}_id) ? null : (int) $row->{info.Name}_id;
        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ColumnMapTag, info.Entity);

            snippet = $@",KEY ind_{info.Entity.Name}_{info.Name}_id ({info.Name}_id)
                        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.KeyMapTag, info.Entity);
        }
    }
}