using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(EntityInfo))]
    public class EntityCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            string snippet = $@"public ?int $id = null;
    ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassPropertyTag, info);

            snippet = $@"$dataStructure->id = (int) $object->ID;
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassParsePropertyTag, info);
        }
    }

    [Export(typeof(IConceptMacro))]
    public class EntityMacroCodeGenerator : IConceptMacro<EntityInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(EntityInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            newConcepts.Add(new DbDeltaInfo
            {
                DataStructure = conceptInfo
            });

            return newConcepts;
        }
    }
}