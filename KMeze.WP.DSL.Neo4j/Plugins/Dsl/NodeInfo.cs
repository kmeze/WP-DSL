using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Node")]
    public class NodeInfo : RepositoryDataStructureInfo
    {
    }
}