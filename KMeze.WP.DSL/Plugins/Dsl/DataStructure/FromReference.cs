using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    ///
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("FromReference")]
    public class FromReferenceInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public ReferencePropertyInfo Reference { get; set; }
    }
}