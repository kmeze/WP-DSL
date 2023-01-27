using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Capability")]
    public class RoleCapabilityInfo : IConceptInfo
    {
        [ConceptKey]
        public RoleInfo Role { get; set; }

        [ConceptKey]
        public CapabilityInfo Capability { get; set; }
    }
}