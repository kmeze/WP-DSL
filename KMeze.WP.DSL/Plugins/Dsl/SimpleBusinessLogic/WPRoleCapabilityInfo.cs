using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Capability")]
    public class WPRoleCapabilityInfo : IConceptInfo
    {
        [ConceptKey]
        public WPRoleInfo Role { get; set; }

        [ConceptKey]
        public CapabilityInfo Capability { get; set; }
    }
}