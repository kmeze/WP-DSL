using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Code")]
    public class CodeInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public string Script { get; set; }
    }
}