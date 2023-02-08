using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

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