using System;
using System.Globalization;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL.Vue.Pinia
{
    [Export(typeof(IWPPluginJsSdkConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(EntityInfo))]
    public class EntityCodeGenerator : IWPPluginJsSdkConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            // Generate entity state in Pinia store
            string snippet = $@"{info.Name}: [],
            ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreStateTag, info.Plugin);

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            // Generate entity actions in Pinia store
            snippet = $@"async get{ti.ToTitleCase(info.Name)}() {{
            const res = await axios.get(`${{this.apiUrl}}/wp-json/{info.Plugin.Slug}/v1/{info.Name}`).then(res => {{
                this.{info.Name} = res.data;
            }})
        }},
        ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreActionTag, info.Plugin);

            snippet = $@"this.{info.Name} = []
            ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreCleanUpActionTag, info.Plugin);
        }
    }
}