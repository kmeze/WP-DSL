using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Capability")]
    public class CapabilityInfo : IConceptInfo
    {
        [ConceptKey]
        public RoleInfo Role { get; set; }

        [ConceptKey]
        public string Cap { get; set; }
    }
}