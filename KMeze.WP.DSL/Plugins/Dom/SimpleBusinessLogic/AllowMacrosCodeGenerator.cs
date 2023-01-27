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
                Name = "AllowLoggedIn",
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

    [Export(typeof(IConceptMacro))]
    public class AllowRoleCodeGenerator : IConceptMacro<AllowRoleInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowRoleInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ActionInfo action = new ActionInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                Name = "AllowRole",
                Script = $@"if (! is_user_logged_in() ) return false;

    $user = wp_get_current_user();
    if (in_array( '{conceptInfo.Role.WPPlugin.Name}_{conceptInfo.Role.Slug}', (array) $user->roles )) return true;

    return false;
",
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
    public class AllowCapabilityCodeGenerator : IConceptMacro<AllowCapabilityInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowCapabilityInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ActionInfo action = new ActionInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                Name = "AllowCapability",
                Script = $@"if (! is_user_logged_in() ) return false;

    if ( current_user_can( '{conceptInfo.Entity.WPPlugin.Name}_{conceptInfo.Capability.Slug}' ) ) return true;

    return false;
",
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
