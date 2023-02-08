using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionPost")]
    public class DataStructureActionPostInfo : IConceptInfo
    {
        [ConceptKey]
        public RepositoryDataStructureInfo DataStructure { get; set; }

        [ConceptKey]
        public CallbackInfo Callback { get; set; }
    }
}