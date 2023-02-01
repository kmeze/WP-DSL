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
        public string SourcePropertyName { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class TakeNameMacro : IConceptMacro<TakeInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(TakeInfo conceptInfo, IDslModel existingConcepts)
        {
            // Find Source ProperyInfo
            var pi = existingConcepts.FindByReference<PropertyInfo>(ci => ci.DataStructure, conceptInfo.List.Source)
                .Where(ci => ci.Name == conceptInfo.SourcePropertyName)
                .SingleOrDefault();

            // TODO: Refactor (create new PropertyInfo and add it to List to generate class property and parse method)
            if (pi is ShortStringPropertyInfo) return new IConceptInfo[] { new ShortStringPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyName } };
            if (pi is IntegerPropertyInfo) return new IConceptInfo[] { new IntegerPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyName } };
            if (pi is BoolPropertyInfo) return new IConceptInfo[] { new BoolPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.SourcePropertyName } };

            return null;
        }
    }
}