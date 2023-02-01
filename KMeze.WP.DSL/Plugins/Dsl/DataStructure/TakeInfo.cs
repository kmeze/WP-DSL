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
        public PropertyInfo SourcePropertyInfo { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class TakeMacro : IConceptMacro<TakeInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(TakeInfo conceptInfo, IDslModel existingConcepts)
        {
            // Create PropertyInfo and add to List to generate DataStructure class properties and parse funcion mapping
            var pi = existingConcepts.FindByReference<PropertyInfo>(ci => ci.DataStructure, conceptInfo.SourcePropertyInfo.DataStructure)
                .Where(ci => ci.Name == conceptInfo.SourcePropertyInfo.Name)
                .SingleOrDefault();

            // TODO: Refactor
            if (pi is ShortStringPropertyInfo) return new IConceptInfo[] { new ShortStringPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyInfo.Name  } };
            if (pi is IntegerPropertyInfo) return new IConceptInfo[] { new IntegerPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyInfo.Name } };
            if (pi is BoolPropertyInfo) return new IConceptInfo[] { new BoolPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyInfo.Name } };
            
            return null;
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Take")]
    public class TakeNameInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public string SourcePropertyName { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class TakeNameMacro : IConceptMacro<TakeNameInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(TakeNameInfo conceptInfo, IDslModel existingConcepts)
        {
            var property = new PropertyInfo
            {
                DataStructure = conceptInfo.List.Source,
                Name = conceptInfo.SourcePropertyName,
            };

            var takeInfo = new TakeInfo
            {
                List = conceptInfo.List,
                SourcePropertyInfo = property,
            };

            return new IConceptInfo[] { property, takeInfo };
        }
    }
}