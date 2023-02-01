using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    /// <summary>
    ///
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Take")]
    public class TakeFromReferenceInfo : IConceptInfo
    {
        [ConceptKey]
        public FromReferenceInfo FromReference { get; set; }

        [ConceptKey]
        public string Name { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class TakeFromReferenceMacro : IConceptMacro<TakeFromReferenceInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(TakeFromReferenceInfo conceptInfo, IDslModel existingConcepts)
        {
            // FromReference.Reference je što (ReferenceProperty u Source DataStructure)
            var re = existingConcepts.FindByReference<ReferencePropertyInfo>(ci => ci.DataStructure, conceptInfo.FromReference.List.Source)
                .Where(ci => ci.Name == conceptInfo.FromReference.SourceReferencePropertyName.Name)
                .SingleOrDefault();

            var pi = existingConcepts.FindByReference<PropertyInfo>(ci => ci.DataStructure, re.ReferencedDataStructure)
                .Where(ci => ci.Name == conceptInfo.Name)
                .SingleOrDefault();

            // TODO: Refactor
            if (pi is ShortStringPropertyInfo) return new IConceptInfo[] { new ShortStringPropertyInfo { DataStructure = conceptInfo.FromReference.List, Name = $@"{re.Name}_{conceptInfo.Name}" } };
            if (pi is IntegerPropertyInfo) return new IConceptInfo[] { new IntegerPropertyInfo { DataStructure = conceptInfo.FromReference.List, Name = $@"{re.Name}_{conceptInfo.Name}" } };
            if (pi is BoolPropertyInfo) return new IConceptInfo[] { new BoolPropertyInfo { DataStructure = conceptInfo.FromReference.List, Name = $@"{re.Name}_{conceptInfo.Name}" } };

            return null;
        }
    }
}