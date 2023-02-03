using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DataStructure")]
    public class DataStructureInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Name { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class DataStructureMacro : IConceptMacro<DataStructureInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(DataStructureInfo conceptInfo, IDslModel existingConcepts)
        {
            var callback = new CallbackInfo
            {
                WPPlugin = conceptInfo.WPPlugin,
                Name = $@"_{conceptInfo.Name}_register_routes",
                Script = $@"( new {conceptInfo.WPPlugin.Name}_{conceptInfo.Name}_REST_Controller() )->register_routes();"
            };

            var actionHook = new ActionHookWithDefaultPriorityInfo
            {
                WPPlugin = conceptInfo.WPPlugin,
                HookName = "rest_api_init",
                Callback = callback,
            };

            return new IConceptInfo[] { callback, actionHook };
        }
    }
}