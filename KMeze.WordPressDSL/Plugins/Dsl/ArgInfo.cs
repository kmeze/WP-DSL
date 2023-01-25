using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WordPressDSL
{
    /// <summary>
    /// Represents Activity argument.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Arg")]
    public class ArgsInfo : IConceptInfo
    {
        [ConceptKey]
        public ActionInfo Action { get; set; }

        [ConceptKey]
        public string Name { get; set; }
    }
}