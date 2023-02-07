using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("MaxLength")]
    public class MaxLengthInfo : IConceptInfo
    {
        [ConceptKey]
        public PropertyInfo Property { get; set; }

        [ConceptKey]
        public string Value { get; set; }
    }
}