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
                    WPPlugin = conceptInfo.DataStructure.WPPlugin,
                    Name = "User",
                };

                var reference = new ReferencePropertyInfo
                {
                    DataStructure = conceptInfo.DataStructure,
                    Name = "Owner",
                    ReferencedDataStructure = user,
                };

                newConcepts.AddRange(new IConceptInfo[] { reference, user });
            }
            else if (conceptInfo.DataStructure is ListInfo)
            {
                var soruceDataStructueOwnerContext = existingConcepts.FindByReference<OwnerContextInfo>(ci => ci.DataStructure, ((ListInfo)conceptInfo.DataStructure).Source);

                if (soruceDataStructueOwnerContext == null)
                    throw new DslSyntaxException(conceptInfo, $@"Source propertty must also apply OwnerContext concept.");
            }
            else
                throw new DslSyntaxException(conceptInfo, $@"OwnerContext can be used only on Entity or List concept.");

            var callback = new CallbackInfo
            {
                WPPlugin = conceptInfo.DataStructure.WPPlugin,
                Name = $@"{conceptInfo.DataStructure.Name}_Filter_OwnerContext",
                Script = $@"$conditions[] = array( 'Name' => 'owner_id', 'Value' => get_current_user_id(), 'Format' => '%d' ); return $conditions;"
            };

            var hook = new FilterHookInfo
            {
                WPPlugin = conceptInfo.DataStructure.WPPlugin,
                HookName = $@"{conceptInfo.DataStructure.WPPlugin.Name}_{conceptInfo.DataStructure.Name}_filter",
                Callback = callback,
                Priority = "20",
                Args = "$conditions"
            };

            newConcepts.AddRange(new IConceptInfo[] { callback, hook });

            return newConcepts;
        }
    }
}