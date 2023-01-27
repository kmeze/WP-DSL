using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Authorization")]
    public class AuthorizationInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }

        [ConceptKey]
        public ActionInfo Action { get; set; }
    }
}