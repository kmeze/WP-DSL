using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
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
}