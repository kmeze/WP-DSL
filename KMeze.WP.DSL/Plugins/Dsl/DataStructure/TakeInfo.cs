using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    ///
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Take")]
    public class TakeInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public PropertyInfo Property { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class TakeMacro : IConceptMacro<TakeInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(TakeInfo conceptInfo, IDslModel existingConcepts)
        {
            var pi = existingConcepts.FindByReference<PropertyInfo>(ci => ci.DataStructure, conceptInfo.Property.DataStructure)
                .Where(ci => ci.Name == conceptInfo.Property.Name)
                .SingleOrDefault();

            // TODO: Refactor
            if (pi is ShortStringPropertyInfo) return new IConceptInfo[] { new ShortStringPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.Property.Name  } };
            if (pi is IntegerPropertyInfo) return new IConceptInfo[] { new IntegerPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.Property.Name } };
            if (pi is BoolPropertyInfo) return new IConceptInfo[] { new BoolPropertyInfo { DataStructure = conceptInfo.List, Name = conceptInfo.Property.Name } };
            
            return null;
        }
    }
}