using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Select")]
    public class SelectInfo : DataStructureInfo
    {
        public string Query { get; set; }
    }
}