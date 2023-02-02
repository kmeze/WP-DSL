using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("WPPlugin")]
    public class WPPluginInfo : IConceptInfo
    {
        [ConceptKey]
        public string Name { get; set; }
    }
}