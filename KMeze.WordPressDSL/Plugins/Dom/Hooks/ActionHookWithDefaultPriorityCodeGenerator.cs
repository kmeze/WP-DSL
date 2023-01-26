using System.Collections.Generic;
using System.ComponentModel.Composition;
using KMeze.WordPressDSL;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IConceptMacro))]
    public class ActionHookWithDefaultPriorityCodeGenerator : IConceptMacro<ActionHookWithDefaultPriorityInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(ActionHookWithDefaultPriorityInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

                newConcepts.Add(new ActionHookInfo
                {
                    WPPlugin = conceptInfo.WPPlugin,
                    HookName = conceptInfo.HookName,
                    Callback = conceptInfo.Callback,
                    Priority = "10",
                    Args = "",
                });

            return newConcepts;
        }
    }
}
