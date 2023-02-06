using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    // TODO: only one default value is allowed on property: else throw DSL syntax exception;
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
        public IntegerPropertyInfo Property { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class DefaultValueZeroMacro : IConceptMacro<DefaultValueZeroInfo>
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

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DefaultEmpty")]
    public class DefaultValueEmptyInfo : IConceptInfo
    {
        [ConceptKey]
        public ShortStringPropertyInfo Property { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class DefaultValueEmptyMacro : IConceptMacro<DefaultValueEmptyInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(DefaultValueEmptyInfo conceptInfo, IDslModel existingConcepts)
        {
            var defaultValue = new DefaultValueInfo
            {
                Property = conceptInfo.Property,
                Value = "''",
            };

            return new IConceptInfo[] { defaultValue };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DefaultTrue")]
    public class DefaultValueTrueInfo : IConceptInfo
    {
        [ConceptKey]
        public BoolPropertyInfo Property { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class DefaultValueTrueMacro : IConceptMacro<DefaultValueTrueInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(DefaultValueTrueInfo conceptInfo, IDslModel existingConcepts)
        {
            var defaultValue = new DefaultValueInfo
            {
                Property = conceptInfo.Property,
                Value = "true",
            };

            return new IConceptInfo[] { defaultValue };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("DefaultFalse")]
    public class DefaultValueFalseInfo : IConceptInfo
    {
        [ConceptKey]
        public BoolPropertyInfo Property { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class DefaultValueFalseMacro : IConceptMacro<DefaultValueFalseInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(DefaultValueFalseInfo conceptInfo, IDslModel existingConcepts)
        {
            var defaultValue = new DefaultValueInfo
            {
                Property = conceptInfo.Property,
                Value = "false",
            };

            return new IConceptInfo[] { defaultValue };
        }
    }
}