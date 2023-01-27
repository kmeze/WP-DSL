using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// Generates a main plugin file
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("JWTAuth")]
    public class JwtAuthInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }
    }
}