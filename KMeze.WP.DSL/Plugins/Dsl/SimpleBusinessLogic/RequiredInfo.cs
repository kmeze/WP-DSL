using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Required")]
    public class RequiredInfo : IConceptInfo
    {
        [ConceptKey]
        public PropertyInfo Property { get; set; }
    }
}