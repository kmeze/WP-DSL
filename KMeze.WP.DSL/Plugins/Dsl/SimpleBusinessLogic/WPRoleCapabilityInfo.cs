using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
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