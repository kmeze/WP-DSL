using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    // TODO: check is Property.DataStructure is Entity
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Unique")]
    public class UniqueInfo : IConceptInfo
    {
        [ConceptKey]
        public PropertyInfo Property { get; set; }
    }
}