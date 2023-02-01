using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("FromReference")]
    public class FromReferenceInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public string SourceReferenceName { get; set; }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("FromReference")]
    public class FromReferenceMacroInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public ReferencePropertyInfo SourceReferencePropertyInfo { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class FromReferenceMacro : IConceptMacro<FromReferenceInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(FromReferenceInfo conceptInfo, IDslModel existingConcepts)
        {
            // Get source ReferencePropertyInfo
            ReferencePropertyInfo rpi = existingConcepts.FindByReference<ReferencePropertyInfo>(ci => ci.DataStructure, conceptInfo.List.Source)
               .Where(ci => ci.Name == conceptInfo.SourceReferenceName)
               .FirstOrDefault();

            // Create FormReferenceInfo
            var reference = new ReferencePropertyInfo
            {
                DataStructure = conceptInfo.List.Source,
                Name = conceptInfo.SourceReferenceName,
                ReferencedDataStructure = rpi.ReferencedDataStructure,
            };

            var fromReferenceInfo = new FromReferenceMacroInfo
            {
                List = conceptInfo.List,
                SourceReferencePropertyInfo = reference,
            };

            return new IConceptInfo[] { reference, fromReferenceInfo };
        }
    }
}