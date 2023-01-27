using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Generates a main plugin file
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("WPPlugin")]
    public class WPPluginInfo : IConceptInfo
    {
        [ConceptKey]
        public string Name { get; set; }
    }
}