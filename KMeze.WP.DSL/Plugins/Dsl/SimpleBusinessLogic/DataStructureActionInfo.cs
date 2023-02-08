﻿using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Action")]
    public class DataStructureActionInfo : IConceptInfo
    {
        [ConceptKey]
        public RepositoryDataStructureInfo DataStructure { get; set; }

        [ConceptKey]
        public CallbackInfo Callback { get; set; }
    }
}