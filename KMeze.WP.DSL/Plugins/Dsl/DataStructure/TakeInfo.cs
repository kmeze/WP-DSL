using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    ///
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Take")]
    public class TakeInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public PropertyInfo Property { get; set; }
    }
}