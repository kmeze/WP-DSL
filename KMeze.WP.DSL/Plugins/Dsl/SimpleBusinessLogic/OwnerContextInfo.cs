using System.ComponentModel.Composition;
using System.Xml.Linq;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("OwnerContext")]
    public class OwnerContextInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class OwnerContextMacro : IConceptMacro<OwnerContextInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(OwnerContextInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            if (conceptInfo.DataStructure is EntityInfo)
            {
                var user = new WPDataStructureInfo
                {
                    Plugin = conceptInfo.DataStructure.Plugin,
                    Name = "User",
                };

                var reference = new ReferencePropertyInfo
                {
                    DataStructure = conceptInfo.DataStructure,
                    Name = "Owner",
                    ReferencedDataStructure = user,
                };

                var required = new RequiredInfo
                {
                    Property = reference,
                };

                newConcepts.AddRange(new IConceptInfo[] { required, reference, user });

                // Add OwnerContext also to all ListInfo DatStructures that uses this Entity as Source
                newConcepts.AddRange(
                    existingConcepts.FindByReference<ListInfo>(ci => ci.Source, conceptInfo.DataStructure)
                        .Select(ci => new OwnerContextInfo { DataStructure = ci })
                );
            }
            else if (conceptInfo.DataStructure is ListInfo)
            {
                var soruceDataStructueOwnerContext = existingConcepts.FindByReference<OwnerContextInfo>(ci => ci.DataStructure, ((ListInfo)conceptInfo.DataStructure).Source);

                if (soruceDataStructueOwnerContext == null)
                    throw new DslSyntaxException(conceptInfo, $@"Source property must also apply the OwnerContext concept.");
            }
            else
                throw new DslSyntaxException(conceptInfo, $@"OwnerContext can be used only on Entity or List concepts.");

            var callback_filter = new CallbackInfo
            {
                Plugin = conceptInfo.DataStructure.Plugin,
                Name = $@"{conceptInfo.DataStructure.Name}_Filter_OwnerContext",
                Script = $@"$conditions[] = array( 'Name' => 'owner_id', 'Value' => get_current_user_id(), 'Format' => '%d' ); return $conditions;"
            };

            var hook_filter = new FilterHookInfo
            {
                WPPlugin = conceptInfo.DataStructure.Plugin,
                Hook = $@"{conceptInfo.DataStructure.Plugin.Slug}_{conceptInfo.DataStructure.Name}_filter",
                Callback = callback_filter,
                Priority = "20",
                Args = "$conditions"
            };

            var callback_insert = new CallbackInfo
            {
                Plugin = conceptInfo.DataStructure.Plugin,
                Name = $@"{conceptInfo.DataStructure.Name}_Insert_OwnerContext",
                Script = $@"$data['Owner_id'] = get_current_user_id(); return $data;"
            };

            var hook_insert = new FilterHookInfo
            {
                WPPlugin = conceptInfo.DataStructure.Plugin,
                Hook = $@"{conceptInfo.DataStructure.Plugin.Slug}_{conceptInfo.DataStructure.Name}_insert",
                Callback = callback_insert,
                Priority = "10",
                Args = "$data"
            };

            var callback_update = new CallbackInfo
            {
                Plugin = conceptInfo.DataStructure.Plugin,
                Name = $@"{conceptInfo.DataStructure.Name}_Update_OwnerContext",
                Script = $@"unset($data['Owner_id']); return $data;"
            };

            var hook_update = new FilterHookInfo
            {
                WPPlugin = conceptInfo.DataStructure.Plugin,
                Hook = $@"{conceptInfo.DataStructure.Plugin.Slug}_{conceptInfo.DataStructure.Name}_update",
                Callback = callback_update,
                Priority = "10",
                Args = "$data"
            };

            newConcepts.AddRange(new IConceptInfo[] { callback_filter, hook_filter, callback_insert, hook_insert, callback_update, hook_update });

            return newConcepts;
        }
    }
}