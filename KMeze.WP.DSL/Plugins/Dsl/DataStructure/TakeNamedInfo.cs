using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Take")]
    public class TakeNamedInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public string PropertyName { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class TakeNamedMacro : IConceptMacro<TakeNamedInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(TakeNamedInfo conceptInfo, IDslModel existingConcepts)
        {
            var property = new PropertyInfo
            {
                DataStructure = conceptInfo.List.Source,
                Name = conceptInfo.PropertyName,
            };

            var takeInfo = new TakeInfo
            {
                List = conceptInfo.List,
                SourcePropertyName = property,
            };

            return new IConceptInfo[] { property, takeInfo };
        }
    }
}