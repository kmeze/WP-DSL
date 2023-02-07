using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents Medium Integer property.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("MediumInteger")]
    public class MediumIntegerPropertyInfo : PropertyInfo
    {
    }
}