using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Select")]
    public class SelectInfo : RepositoryDataStructureInfo
    {
        public string Query { get; set; }
    }
}