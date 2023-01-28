using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;
using KMeze.WP.DSL;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(BoolPropertyInfo))]
    public class BoolPropertyCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (BoolPropertyInfo)conceptInfo;

            string snippet = $@"public ?bool ${info.Name} = null;
    ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ClassPropertyTag, info.DataStructure);

            snippet = $@",{info.Name} BOOLEAN
                        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ColumnTag, info.DataStructure);

            snippet = $@"$entity->{info.Name} = is_null($row->{info.Name}) ? null : (bool) $row->{info.Name};
        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ColumnMapTag, info.DataStructure);
        }
    }
}