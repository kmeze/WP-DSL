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
            var actionHook = new ActionHookWithAnonymousCallbackWithDefaultPriorityInfo
            {
                Plugin = conceptInfo.Plugin,
                Hook = "rest_api_init",
                Script = $@"fn () => ( new {conceptInfo.Plugin.Slug}_{conceptInfo.Name}_REST_Controller() )->register_routes()",
            };

            return new IConceptInfo[] { actionHook };
        }
    }
}