using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents Integer property.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Integer")]
    public class IntegerPropertyInfo : PropertyInfo, IConceptInfo
    {
    }
}