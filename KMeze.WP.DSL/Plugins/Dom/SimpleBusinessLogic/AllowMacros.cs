using System.Collections.Generic;
using System.ComponentModel.Composition;
using KMeze.WP.DSL;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptMacro))]
    public class AllowAllMacro : IConceptMacro<AllowAllInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowAllInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ActionInfo action = new ActionInfo
            {
                WPPlugin = conceptInfo.DataStructure.WPPlugin,
                Name = "AllowAll",
                Script = "return true;",
            };

            newConcepts.Add(action);

            newConcepts.Add(new AuthorizationInfo
            {
                DataStructure = conceptInfo.DataStructure,
                Action = action,
            });

            return newConcepts;
        }
    }

    [Export(typeof(IConceptMacro))]
    public class AllowLoggedInMacro : IConceptMacro<AllowLoggedInInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowLoggedInInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ActionInfo action = new ActionInfo
            {
                WPPlugin = conceptInfo.DataStructure.WPPlugin,
                Name = "AllowLoggedIn",
                Script = "return is_user_logged_in();",
            };

            newConcepts.Add(action);

            newConcepts.Add(new AuthorizationInfo
            {
                DataStructure = conceptInfo.DataStructure,
                Action = action,
            });

            return newConcepts;
        }
    }

    [Export(typeof(IConceptMacro))]
    public class AllowRoleMacro : IConceptMacro<AllowRoleInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowRoleInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ActionInfo action = new ActionInfo
            {
                WPPlugin = conceptInfo.DataStructure.WPPlugin,
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
                DataStructure = conceptInfo.DataStructure,
                Action = action,
            });

            return newConcepts;
        }
    }

    [Export(typeof(IConceptMacro))]
    public class AllowCapabilityMacro : IConceptMacro<AllowCapabilityInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowCapabilityInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ActionInfo action = new ActionInfo
            {
                WPPlugin = conceptInfo.DataStructure.WPPlugin,
                Name = "AllowCapability",
                Script = $@"if (! is_user_logged_in() ) return false;

    if ( current_user_can( '{conceptInfo.DataStructure.WPPlugin.Name}_{conceptInfo.Capability.Slug}' ) ) return true;

    return false;
",
            };

            newConcepts.Add(action);

            newConcepts.Add(new AuthorizationInfo
            {
                DataStructure = conceptInfo.DataStructure,
                Action = action,
            });

            return newConcepts;
        }
    }
}