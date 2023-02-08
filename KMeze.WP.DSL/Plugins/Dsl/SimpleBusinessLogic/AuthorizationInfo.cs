using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Authorization")]
    public class AuthorizationInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }

        [ConceptKey]
        public CallbackInfo Action { get; set; }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowAll")]
    public class AllowAllInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class AllowAllMacro : IConceptMacro<AllowAllInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowAllInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            CallbackInfo action = new CallbackInfo
            {
                WPPlugin = conceptInfo.DataStructure.Plugin,
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

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowLoggedIn")]
    public class AllowLoggedInInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class AllowLoggedInMacro : IConceptMacro<AllowLoggedInInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowLoggedInInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            CallbackInfo action = new CallbackInfo
            {
                WPPlugin = conceptInfo.DataStructure.Plugin,
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

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowRole")]
    public class AllowRoleInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }

        [ConceptKey]
        public RoleInfo Role { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class AllowRoleMacro : IConceptMacro<AllowRoleInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowRoleInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            CallbackInfo action = new CallbackInfo
            {
                WPPlugin = conceptInfo.DataStructure.Plugin,
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

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AllowCapability")]
    public class AllowCapabilityInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }

        [ConceptKey]
        public CapabilityInfo Capability { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class AllowCapabilityMacro : IConceptMacro<AllowCapabilityInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AllowCapabilityInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            CallbackInfo action = new CallbackInfo
            {
                WPPlugin = conceptInfo.DataStructure.Plugin,
                Name = "AllowCapability",
                Script = $@"if (! is_user_logged_in() ) return false;

    if ( current_user_can( '{conceptInfo.DataStructure.Plugin.Name}_{conceptInfo.Capability.Slug}' ) ) return true;

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