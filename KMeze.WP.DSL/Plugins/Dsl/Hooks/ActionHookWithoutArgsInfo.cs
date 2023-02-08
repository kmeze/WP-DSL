using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookWithoutArgsInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public string Hook { get; set; }

        [ConceptKey]
        public CallbackInfo Callback { get; set; }

        [ConceptKey]
        public string Priority { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class ActionHookWithoutMacro : IConceptMacro<ActionHookWithoutArgsInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(ActionHookWithoutArgsInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            newConcepts.Add(new ActionHookInfo
            {
                Plugin = conceptInfo.Plugin,
                Hook = conceptInfo.Hook,
                Callback = conceptInfo.Callback,
                Priority = conceptInfo.Priority,
                Args = "",
            });

            return newConcepts;
        }
    }
}