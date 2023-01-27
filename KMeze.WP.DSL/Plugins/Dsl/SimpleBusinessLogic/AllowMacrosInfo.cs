using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowAll")]
    public class AllowAllInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowLoggedIn")]
    public class AllowLoggedInInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowRole")]
    public class AllowRoleInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }

        [ConceptKey]
        public RoleInfo Role { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowCapability")]
    public class AllowCapabilityInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }

        [ConceptKey]
        public CapabilityInfo Capability { get; set; }
    }
}