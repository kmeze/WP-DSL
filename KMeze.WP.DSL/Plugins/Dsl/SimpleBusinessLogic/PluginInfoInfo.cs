using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("PluginInfo")]
    public class PluginInfoInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}