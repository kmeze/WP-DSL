using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    /// <summary>
    /// Drop entity table on plugin deactivation.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DropOnDeactivate")]
    public class DropOnDeactivateInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }
    }
}