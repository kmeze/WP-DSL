using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    // TODO: Enable to all DataStructures
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Enum")]
    public class EnumPropertyInfo : PropertyInfo
    {
        public string Values { get; set; }
    }
}