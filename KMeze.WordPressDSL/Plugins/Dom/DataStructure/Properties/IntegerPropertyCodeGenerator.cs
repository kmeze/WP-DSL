using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(IntegerPropertyInfo))]
    public class IntegerPropertyCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (IntegerPropertyInfo)conceptInfo;

            string snippet = $@"public ?int ${info.Name} = null;
    ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ClassPropertyTag, info.DataStructure);

            snippet = $@",{info.Name} INTEGER
                        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ColumnTag, info.DataStructure);

            snippet = $@"$entity->{info.Name} = is_null($row->{info.Name}) ? null : (int) $row->{info.Name};
        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.ColumnMapTag, info.DataStructure);
        }
    }
}