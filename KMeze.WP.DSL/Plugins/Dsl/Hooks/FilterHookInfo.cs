using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("FilterHook")]
    public class FilterHookInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Hook { get; set; }

        [ConceptKey]
        public CallbackInfo Callback { get; set; }

        [ConceptKey]
        public string Priority { get; set; }

        [ConceptKey]
        public string Args { get; set; }
    }
}