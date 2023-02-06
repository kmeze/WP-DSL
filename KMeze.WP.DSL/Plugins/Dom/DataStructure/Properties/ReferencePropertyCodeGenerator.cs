using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
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
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassPropertyTag, info.DataStructure);

            snippet = $@"$dataStructure->{info.Name}_id = is_null($object->{info.Name}_id) ? null : (int) $object->{info.Name}_id;
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.DataClassParsePropertyTag, info.DataStructure);

            if (info.DataStructure is EntityInfo)
            {
                snippet = $@",{info.Name}_id BIGINT(20) UNSIGNED";
                codeBuilder.InsertCode(snippet, PropertyCodeGenerator.DbDeltaPropertyColumnTag, info);

                snippet = $@",KEY ind_{info.DataStructure.Name}_{info.Name}_id ({info.Name}_id)
                        ";
                codeBuilder.InsertCode(snippet, EntityDbDeltaCodeGenerator.DbDeltaKeyTag, info.DataStructure);
            }
        }
    }
}