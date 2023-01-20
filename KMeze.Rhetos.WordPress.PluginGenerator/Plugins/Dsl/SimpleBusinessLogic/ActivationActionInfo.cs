﻿using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    /// <summary>
    /// Represents custom entity.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("ActivationAction")]
    public class ActivationActionInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo WPPlugin { get; set; }

        [ConceptKey]
        public ActionInfo Action { get; set; }
    }
}