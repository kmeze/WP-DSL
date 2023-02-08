using System.ComponentModel.Composition;
using NLog.Filters;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Filter")]
    public class FilterInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }

        [ConceptKey]
        public string Name { get; set; }

        [ConceptKey]
        public string PropertyName { get; set; }

        [ConceptKey]
        public string PropertyValue { get; set; }

        [ConceptKey]
        public string Format { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class FilterMacro : IConceptMacro<FilterInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(FilterInfo conceptInfo, IDslModel existingConcepts)
        {
            var callback = new CallbackInfo
            {
                Plugin = conceptInfo.Entity.Plugin,
                Name = conceptInfo.Name,
                Script = $@"$conditions[] = array( 'Name' => '{conceptInfo.PropertyName}', 'Value' => '{conceptInfo.PropertyValue}', 'Format' => '{conceptInfo.Format}' );

                    return $conditions;
                ",
            };

            var filterHook = new FilterHookInfo
            {
                WPPlugin = conceptInfo.Entity.Plugin,
                Hook = $@"{conceptInfo.Entity.Plugin.Slug}_{conceptInfo.Entity.Name}_filter",
                Callback = callback,
                Priority = "DefaultPriority",
                Args = "$conditions"
            };

            return new IConceptInfo[] { callback, filterHook };
        }
    }
}