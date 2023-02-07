using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    // TODO: Enable to all DataStructures
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Enum")]
    public class EnumInfo : PropertyInfo
    {
        public string Values { get; set; }
    }
}