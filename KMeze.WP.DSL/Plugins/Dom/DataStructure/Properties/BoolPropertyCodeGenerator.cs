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
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassPropertyTag, info.DataStructure);

            snippet = $@"$dataStructure->{info.Name} = is_null($object->{info.Name}) ? null : (bool) $object->{info.Name};
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassParsePropertyTag, info.DataStructure);

            if (info.DataStructure is RepositoryDataStructureInfo)
            {

                snippet = $@"if ( $params['{info.Name}'] !== null ) $conditions[] = array( 'Name' => '{info.Name}', 'Value' => $params['{info.Name}'] === strtolower('null') ? 'null' : (int) $params['{info.Name}'], 'Format' => '%d' );
            ";
                codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerClassParamToConditionTag, info.DataStructure);
            }

            if (info.DataStructure is EntityInfo)
            {
                snippet = $@",{info.Name} BOOLEAN";
                codeBuilder.InsertCode(snippet, PropertyCodeGenerator.DbDeltaPropertyColumnNameTag, info);
            }
        }
    }
}