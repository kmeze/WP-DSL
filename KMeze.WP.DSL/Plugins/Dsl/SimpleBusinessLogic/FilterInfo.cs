﻿using System.ComponentModel.Composition;
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
            var newConcepts = new List<IConceptInfo>();

            var callback = new CallbackInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                Name = conceptInfo.Name,
                Script = $@"return array(array( 'Name' => '{conceptInfo.PropertyName}', 'Value' => '{conceptInfo.PropertyValue}', 'Format' => '{conceptInfo.Format}' ));",
            };

            var filterHook = new FilterHookInfo
            {
                WPPlugin = conceptInfo.Entity.WPPlugin,
                HookName = $@"{conceptInfo.Entity.WPPlugin.Name}_{conceptInfo.Entity.Name}_filter",
                Callback = callback,
                Priority = "DefaultPriority",
                Args = "$conditions"
            };

            return new IConceptInfo[] { callback, filterHook };
        }
    }
}