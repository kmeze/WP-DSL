using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string HookName { get; set; }

        [ConceptKey]
        public ActionInfo Callback { get; set; }

        [ConceptKey]
        public string Priority { get; set; }

        [ConceptKey]
        public string Args { get; set; }
    }
}