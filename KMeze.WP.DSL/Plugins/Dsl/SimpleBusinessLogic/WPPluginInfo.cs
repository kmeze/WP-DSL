using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("WPPlugin")]
    public class WPPluginInfo : IConceptInfo
    {
        [ConceptKey]
        public string Name { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class WPPluginMacro : IConceptMacro<WPPluginInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(WPPluginInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();
            newConcepts.Add(new WPDataStructureInfo
            {
                WPPlugin = conceptInfo,
                Name = "User",
            });

            return newConcepts;
        }
    }
}