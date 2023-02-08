using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("RepositoryDataStructure")]
    public class RepositoryDataStructureInfo : DataStructureInfo
    {
    }

    [Export(typeof(IConceptMacro))]
    public class RepositoryDataStructureMacro : IConceptMacro<RepositoryDataStructureInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(RepositoryDataStructureInfo conceptInfo, IDslModel existingConcepts)
        {
            var callback = new CallbackInfo
            {
                WPPlugin = conceptInfo.Plugin,
                Name = $@"{conceptInfo.Name}_register_routes",
                Script = $@"( new {conceptInfo.Plugin.Name}_{conceptInfo.Name}_REST_Controller() )->register_routes();"
            };

            var actionHook = new ActionHookWithDefaultPriorityInfo
            {
                Plugin = conceptInfo.Plugin,
                Hook = "rest_api_init",
                Callback = callback,
            };

            return new IConceptInfo[] { callback, actionHook };
        }
    }
}