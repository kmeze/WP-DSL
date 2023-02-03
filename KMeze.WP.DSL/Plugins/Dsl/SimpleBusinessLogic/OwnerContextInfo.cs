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
        public EntityInfo Entity { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class OwnerContextMacro : IConceptMacro<OwnerContextInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(OwnerContextInfo conceptInfo, IDslModel existingConcepts)
        {
            var user = new WPDataStructureInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                Name = "User",
            };

            var reference = new ReferencePropertyInfo
            {
                DataStructure = conceptInfo.Entity,
                Name = "Owner",
                ReferencedDataStructure = user,
            };

            var callback = new CallbackInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                Name = $@"{conceptInfo.Entity.Name}_Filter_OwnerContext",
                Script = $@"$conditions[] = array( 'Name' => 'owner_id', 'Value' => get_current_user_id(), 'Format' => '%d' ); return $conditions;"
            };

            var hook = new FilterHookInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                HookName = "TestPlugin_TestEntity_filter",
                Callback = callback,
                Priority = "20",
                Args = "$conditions"
            };

            return new IConceptInfo[] { user, reference, callback, hook };
        }
    }
}