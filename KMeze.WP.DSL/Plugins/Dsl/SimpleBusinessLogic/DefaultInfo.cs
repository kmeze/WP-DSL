using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Default")]
    public class DefaultInfo : IConceptInfo
    {
        [ConceptKey]
        public PropertyInfo Property { get; set; }

        public string Expression { get; set; }
    }
}