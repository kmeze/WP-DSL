using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    ///
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("List")]
    public class ListInfo : DataStructureInfo
    {
        public DataStructureInfo Source { get; set; }
    }
}