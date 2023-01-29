using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// RepositoryInfo is an abstract concept: there is no ConceptKeyword.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    public class RepositoryInfo : IConceptInfo
    {
        [ConceptKey]
        public DataStructureInfo DataStructure { get; set; }
    }
}