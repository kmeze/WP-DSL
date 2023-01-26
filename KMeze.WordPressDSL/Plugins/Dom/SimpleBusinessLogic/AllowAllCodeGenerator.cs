using System.Collections.Generic;
using System.ComponentModel.Composition;
using KMeze.WordPressDSL;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IConceptMacro))]
    public class AllowAllCodeGenerator : IConceptMacro<AllowAllInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowAllInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ActionInfo action = new ActionInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                Name = "AllowAll",
                Script = "return true;",
            };

            newConcepts.Add(action);

            newConcepts.Add(new AuthorizationInfo
            {
                Entity = conceptInfo.Entity,
                Action = action,
            });

            return newConcepts;
        }
    }

    [Export(typeof(IConceptMacro))]
    public class AllowLoggedInCodeGenerator : IConceptMacro<AllowLoggedInInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowLoggedInInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ActionInfo action = new ActionInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                Name = "AllowAll",
                Script = "return is_user_logged_in();",
            };

            newConcepts.Add(action);

            newConcepts.Add(new AuthorizationInfo
            {
                Entity = conceptInfo.Entity,
                Action = action,
            });

            return newConcepts;
        }
    }
}
