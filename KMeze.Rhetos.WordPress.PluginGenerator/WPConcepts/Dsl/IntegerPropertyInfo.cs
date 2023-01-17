using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.Rhetos.WordPress.PluginGenerator
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