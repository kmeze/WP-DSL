using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents ShotString property.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ShortString")]
    public class ShortStringPropertyInfo : PropertyInfo, IConceptInfo
    {
    }
}