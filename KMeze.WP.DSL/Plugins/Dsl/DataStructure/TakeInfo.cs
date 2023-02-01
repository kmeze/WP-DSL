using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Take")]
    public class TakeInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public PropertyInfo SourcePropertyName { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class TakeMacro : IConceptMacro<TakeInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(TakeInfo conceptInfo, IDslModel existingConcepts)
        {
            // Create PropertyInfo and add to List to generate DataStructure class properties and parse funcion mapping
            var pi = existingConcepts.FindByReference<PropertyInfo>(ci => ci.DataStructure, conceptInfo.SourcePropertyName.DataStructure)
                .Where(ci => ci.Name == conceptInfo.SourcePropertyName.Name)
                .SingleOrDefault();

            // TODO: Refactor
            if (pi is ShortStringPropertyInfo) return new IConceptInfo[] { new ShortStringPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyName.Name  } };
            if (pi is IntegerPropertyInfo) return new IConceptInfo[] { new IntegerPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyName.Name } };
            if (pi is BoolPropertyInfo) return new IConceptInfo[] { new BoolPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyName.Name } };
            
            return null;
        }
    }

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