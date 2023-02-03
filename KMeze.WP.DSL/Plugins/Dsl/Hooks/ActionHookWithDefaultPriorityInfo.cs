using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookWithDefaultPriorityInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string HookName { get; set; }

        [ConceptKey]
        public CallbackInfo Callback { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class ActionHookWithDefaultPriorityMacro : IConceptMacro<ActionHookWithDefaultPriorityInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(ActionHookWithDefaultPriorityInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            newConcepts.Add(new ActionHookInfo
            {
                WPPlugin = conceptInfo.WPPlugin,
                HookName = conceptInfo.HookName,
                Callback = conceptInfo.Callback,
                Priority = "DefaultPriority",
                Args = "",
            });

            return newConcepts;
        }
    }
}