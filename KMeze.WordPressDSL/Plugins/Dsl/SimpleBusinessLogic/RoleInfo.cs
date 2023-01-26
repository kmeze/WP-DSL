using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Role")]
    public class RoleInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public string Slug { get; set; }

        [ConceptKey]
        public string Name { get; set; }
    }
}