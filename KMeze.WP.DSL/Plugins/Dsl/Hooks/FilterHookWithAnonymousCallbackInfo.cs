using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("FilterHook")]
    public class FilterHookWithAnonymousCallbackInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Hook { get; set; }

        [ConceptKey]
        public string Script { get; set; }

        public string Priority { get; set; }

        public string Args { get; set; }
    }
}