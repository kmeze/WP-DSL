using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("List")]
    public class ListInfo : RepositoryDataStructureInfo
    {
        public DataStructureInfo Source { get; set; }
    }
}