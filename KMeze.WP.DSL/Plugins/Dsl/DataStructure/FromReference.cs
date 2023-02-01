using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("FromReference")]
    public class FromReferenceInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public ReferencePropertyInfo SourceReferencePropertyInfo { get; set; }
    }
}