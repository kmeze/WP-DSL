using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DefaultValue")]
    public class DefaultValueInfo : IConceptInfo
    {
        [ConceptKey]
        public PropertyInfo Property { get; set; }

        [ConceptKey]
        public string Value { get; set; }
    }
}