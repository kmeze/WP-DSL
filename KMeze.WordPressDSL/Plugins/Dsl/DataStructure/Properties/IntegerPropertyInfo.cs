using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
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