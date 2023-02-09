using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookWithAnonymousCallbackInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public string Hook { get; set; }

        [ConceptKey]
        public string Script { get; set; }

        public string Priority { get; set; }

        public string AcceptedArgs { get; set; }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookWithAnonymousCallbackWithoutArgsInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public string Hook { get; set; }

        [ConceptKey]
        public string Script { get; set; }

        public string Priority { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class ActionHookWithAnonymousCallbackWithoutArgsMacro : IConceptMacro<ActionHookWithAnonymousCallbackWithoutArgsInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(ActionHookWithAnonymousCallbackWithoutArgsInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            newConcepts.Add(new ActionHookWithAnonymousCallbackInfo
            {
                Plugin = conceptInfo.Plugin,
                Hook = conceptInfo.Hook,
                Script = conceptInfo.Script,
                Priority = conceptInfo.Priority,
                AcceptedArgs = "0",
            });

            return newConcepts;
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookWithAnonymousCallbackWithDefaultPriorityInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public string Hook { get; set; }

        [ConceptKey]
        public string Script { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class ActionHookWithAnonymousCallbackWithDefaultPriorityMacro : IConceptMacro<ActionHookWithAnonymousCallbackWithDefaultPriorityInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(ActionHookWithAnonymousCallbackWithDefaultPriorityInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            newConcepts.Add(new ActionHookWithAnonymousCallbackInfo
            {
                Plugin = conceptInfo.Plugin,
                Hook = conceptInfo.Hook,
                Script = conceptInfo.Script,
                Priority = "DefaultPriority",
                AcceptedArgs = "0",
            });

            return newConcepts;
        }
    }
}