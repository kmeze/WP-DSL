using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("FromReference")]
    public class FromNamedReferenceInfo : IConceptInfo
    {
        [ConceptKey]
        public ListInfo List { get; set; }

        [ConceptKey]
        public string Name { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class FromNamedReferenceMacro : IConceptMacro<FromNamedReferenceInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(FromNamedReferenceInfo conceptInfo, IDslModel existingConcepts)
        {
            ReferencePropertyInfo rpi = existingConcepts.FindByReference<ReferencePropertyInfo>(ci => ci.DataStructure, conceptInfo.List.Source)
               .Where(ci => ci.Name == conceptInfo.Name)
               .FirstOrDefault();

            var reference = new ReferencePropertyInfo
            {
                DataStructure = conceptInfo.List.Source,
                Name = conceptInfo.Name,
                Referenced = rpi.Referenced,
            };

            var fromReferenceInfo = new FromReferenceInfo
            {
                List = conceptInfo.List,
                Reference = reference,
            };

            return new IConceptInfo[] { reference, fromReferenceInfo };
        }
    }
}