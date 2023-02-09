using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(CharPropertyInfo))]
    public class CharPropertyCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (CharPropertyInfo)conceptInfo;

            string snippet = $@"public ?string ${info.Name} = null;
    ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassPropertyTag, info.DataStructure);

            snippet = $@"$dataStructure->{info.Name} = $object->{info.Name};
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassParsePropertyTag, info.DataStructure);

            if (info.DataStructure is RepositoryDataStructureInfo)
            {

                snippet = $@"if ( $params['{info.Name}'] !== null ) $conditions[] = array( 'Name' => '{info.Name}', 'Value' => $params['{info.Name}'], 'Format' => '%s' );
            ";
                codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassParamToConditionTag, info.DataStructure);
            }

            if (info.DataStructure is EntityInfo)
            {
                snippet = $@",{info.Name} CHAR";
                codeBuilder.InsertCode(snippet, PropertyCodeGenerator.DbDeltaPropertyColumnNameTag, info);
            }
        }
    }
}