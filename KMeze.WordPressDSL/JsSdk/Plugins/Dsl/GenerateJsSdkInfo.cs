using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// Generates a main plugin file
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("GenerateJsSdk")]
    public class GenerateJsSdkInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }
    }
}