using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    ///
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Take")]
    public class TakeFromReferenceInfo : IConceptInfo
    {
        [ConceptKey]
        public FromReferenceInfo Reference { get; set; }

        [ConceptKey]
        public PropertyInfo Property { get; set; }
    }
}