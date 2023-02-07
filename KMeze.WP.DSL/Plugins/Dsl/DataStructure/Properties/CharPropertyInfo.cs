using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents Char property.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Char")]
    public class CharPropertyInfo : PropertyInfo
    {
    }
}