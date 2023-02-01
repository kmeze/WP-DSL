using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Action")]
    public class DataStructureActionInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }

        [ConceptKey]
        public ActionInfo Action { get; set; }
    }
}