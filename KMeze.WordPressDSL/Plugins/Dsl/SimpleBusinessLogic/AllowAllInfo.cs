using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowAll")]
    public class AllowAllInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }
    }
}