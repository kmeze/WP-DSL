using System.ComponentModel.Composition;
using Rhetos.Dsl;
using KMeze.WP.DSL;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents Amount month property.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Bool")]
    public class BoolPropertyInfo : PropertyInfo, IConceptInfo
    {
    }
}