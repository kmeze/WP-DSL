using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents entity 1:n reference.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Reference")]
    public class ReferencePropertyInfo : PropertyInfo, IConceptInfo
    {
        public DataStructureInfo ReferencedDataStructure { get; set; }
    }
}