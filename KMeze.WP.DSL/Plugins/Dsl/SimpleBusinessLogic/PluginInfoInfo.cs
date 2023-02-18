using System.ComponentModel.Composition;
using KMeze.WP.DSL;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("PluginInfo")]
    public class PluginInfoInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        [ConceptKey]
        public string Key { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Name")]
    public class PluginNameInfoInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class PluginNameInfoMacro : IConceptMacro<PluginNameInfoInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(PluginNameInfoInfo conceptInfo, IDslModel existingConcepts)
        {
            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "Plugin Name",
                Value = conceptInfo.Value,
            } };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Description")]
    public class PluginDescriptionInfoInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class PluginDescriptionInfoMacro : IConceptMacro<PluginDescriptionInfoInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(PluginDescriptionInfoInfo conceptInfo, IDslModel existingConcepts)
        {
            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "Description",
                Value = conceptInfo.Value,
            } };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Version")]
    public class VersionInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class VersionMacro : IConceptMacro<VersionInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(VersionInfo conceptInfo, IDslModel existingConcepts)
        {
            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "Version",
                Value = conceptInfo.Value,
            } };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("PluginURI")]
    public class PluginURIInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class PluginURIMacro : IConceptMacro<PluginURIInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(PluginURIInfo conceptInfo, IDslModel existingConcepts)
        {
            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "PluginUri",
                Value = conceptInfo.Value,
            } };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("Author")]
    public class AuthorInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class AuthorMacro : IConceptMacro<AuthorInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AuthorInfo conceptInfo, IDslModel existingConcepts)
        {
            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "Author",
                Value = conceptInfo.Value,
            } };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AuthorURI")]
    public class AuthorURIInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class AuthorURIMacro : IConceptMacro<AuthorURIInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AuthorURIInfo conceptInfo, IDslModel existingConcepts)
        {
            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "Author URI",
                Value = conceptInfo.Value,
            } };
        }
    }
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("RequiresAtLeast")]
    public class RequiresAtLeastInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class RequiresAtLeastMacro : IConceptMacro<RequiresAtLeastInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(RequiresAtLeastInfo conceptInfo, IDslModel existingConcepts)
        {
            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "Requires at least",
                Value = conceptInfo.Value,
            } };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("RequiresPHP")]
    public class RequiresPHPInfo : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }

        public string Value { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class RequiresPHPMacro : IConceptMacro<RequiresPHPInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(RequiresPHPInfo conceptInfo, IDslModel existingConcepts)
        {
            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "Requires PHP",
                Value = conceptInfo.Value,
            } };
        }
    }

    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("AGPLv3")]
    public class AGPLv3Info : IConceptInfo
    {
        [ConceptKey]
        public WPPluginInfo Plugin { get; set; }
    }

    [Export(typeof(IConceptMacro))]
    public class AGPLv3Macro : IConceptMacro<AGPLv3Info>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(AGPLv3Info conceptInfo, IDslModel existingConcepts)
        {

            return new IConceptInfo[] { new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "License",
                Value = "AGPL v3",
            },
            new PluginInfoInfo
            {
                Plugin = conceptInfo.Plugin,
                Key = "License URI",
                Value = "https://www.gnu.org/licenses/agpl-3.0.html",
            }};
        }
    }
}