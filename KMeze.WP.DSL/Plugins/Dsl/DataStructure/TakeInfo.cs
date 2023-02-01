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
}