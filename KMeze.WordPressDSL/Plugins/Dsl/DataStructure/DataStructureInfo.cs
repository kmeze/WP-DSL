using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// DataStructure is an abstract concept: there is no ConceptKeyword.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    public class DataStructureInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Name { get; set; }
    }
}