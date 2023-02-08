using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DeactivationHook")]
    public class DeactivationHookInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public CallbackInfo Callback { get; set; }
    }
}