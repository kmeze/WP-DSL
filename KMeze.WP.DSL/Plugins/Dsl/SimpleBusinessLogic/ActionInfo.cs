using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Action")]
    public class ActionInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Name { get; set; }

        public string Script { get; set; }
    }
}