using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DeactivationAction")]
    public class DectivationActionInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public ActionInfo Action { get; set; }
    }
}