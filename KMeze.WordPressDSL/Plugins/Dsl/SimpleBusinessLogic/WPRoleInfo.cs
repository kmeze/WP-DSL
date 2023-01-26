using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// .
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Role")]
    public class WPRoleInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Slug { get; set; }
    }
}