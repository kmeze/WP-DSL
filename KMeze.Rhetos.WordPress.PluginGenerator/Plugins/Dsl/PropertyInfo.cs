using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    /// <summary>
    /// Property is an abstract concept: there is no ConceptKeyword.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    public class PropertyInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }

        [ConceptKey]
        public string Name { get; set; }
    }
}