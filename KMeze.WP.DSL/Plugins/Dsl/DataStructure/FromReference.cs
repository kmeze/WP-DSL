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
        public ReferencePropertyInfo SourceReferencePropertyInfo { get; set; }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("FromReference")]
    public class FromReferenceNameInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public string SourceReferenceName { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class FromNamedReferenceMacro : IConceptMacro<FromReferenceNameInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(FromReferenceNameInfo conceptInfo, IDslModel existingConcepts)
        {
            ReferencePropertyInfo rpi = existingConcepts.FindByReference<ReferencePropertyInfo>(ci => ci.DataStructure, conceptInfo.List.Source)
               .Where(ci => ci.Name == conceptInfo.SourceReferenceName)
               .FirstOrDefault();

            var reference = new ReferencePropertyInfo
            {
                DataStructure = conceptInfo.List.Source,
                Name = conceptInfo.SourceReferenceName,
                ReferencedDataStructure = rpi.ReferencedDataStructure,
            };

            var fromReferenceInfo = new FromReferenceInfo
            {
                List = conceptInfo.List,
                SourceReferencePropertyInfo = reference,
            };

            return new IConceptInfo[] { reference, fromReferenceInfo };
        }
    }
}