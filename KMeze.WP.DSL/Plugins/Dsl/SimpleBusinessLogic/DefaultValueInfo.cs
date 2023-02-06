using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DefaultValue")]
    public class DefaultValueInfo : IConceptInfo
    {
        [ConceptKey]
        public PropertyInfo Property { get; set; }

        [ConceptKey]
        public string Value { get; set; }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DefaultZero")]
    public class DefaultValueZeroInfo : IConceptInfo
    {
        [ConceptKey]
        public PropertyInfo Property { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class DefaultZeroMacro : IConceptMacro<DefaultValueZeroInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(DefaultValueZeroInfo conceptInfo, IDslModel existingConcepts)
        {
            var defaultValue = new DefaultValueInfo
            {
                Property = conceptInfo.Property,
                Value = "0",
            };

            return new IConceptInfo[] { defaultValue };
        }
    }
}