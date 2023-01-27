using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Code")]
    public class CodeInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Script { get; set; }
    }
}