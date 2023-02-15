using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("SqlProc")]
    public class SqlProcInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public string Name { get; set; }

        public string Args { get; set; }

        public string Script { get; set; }
    }
}