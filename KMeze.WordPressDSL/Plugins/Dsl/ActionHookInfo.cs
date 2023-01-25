using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActionHook")]
    public class ActionHookInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Slug { get; set; }

        [ConceptKey]
        public ActionInfo Callback { get; set; }

        public string Priority { get; set; }
    }
}