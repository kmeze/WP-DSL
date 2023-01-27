using System.Collections.Generic;
using System.ComponentModel.Composition;
using KMeze.WP.DSL;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptMacro))]
    public class ActionHookWithoutCodeGenerator : IConceptMacro<ActionHookWithoutArgsInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(ActionHookWithoutArgsInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

                newConcepts.Add(new ActionHookInfo
                {
                    WPPlugin = conceptInfo.WPPlugin,
                    HookName = conceptInfo.HookName,
                    Callback = conceptInfo.Callback,
                    Priority = conceptInfo.Priority,
                    Args = "",
                });

            return newConcepts;
        }
    }
}
