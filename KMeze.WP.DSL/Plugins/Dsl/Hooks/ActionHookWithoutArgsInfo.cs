using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookWithoutArgsInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string HookName { get; set; }

        [ConceptKey]
        public ActionInfo Callback { get; set; }

        [ConceptKey]
        public string Priority { get; set; }
    }
}