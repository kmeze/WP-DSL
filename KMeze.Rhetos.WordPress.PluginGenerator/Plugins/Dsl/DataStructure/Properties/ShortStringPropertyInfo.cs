using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    /// <summary>
    /// Represents ShotrString property.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ShortString")]
    public class ShortStringPropertyInfo : PropertyInfo, IConceptInfo
    {
    }
}