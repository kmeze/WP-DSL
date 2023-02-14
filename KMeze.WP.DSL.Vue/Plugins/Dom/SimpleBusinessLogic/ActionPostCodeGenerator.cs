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
    [ExportMetadata(MefProvider.Implements, typeof(ActionPostInfo))]
    public class ActionPostCodeGenerator : IWPPluginJsSdkConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ActionPostInfo)conceptInfo;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            // Generate entity actions in Pinia store
            string snippet = $@"async post{ti.ToTitleCase(info.DataStructure.Name)}_{info.Callback.Name}(query, data) {{
            let ret = null
            await axios.post(`${{this.apiUrl}}/wp-json/{info.Callback.Name}/v1/{info.DataStructure.Name}/{info.Callback.Name}?${{query}}`, data).then(res => {{
                ret = res.data
            }})

            return ret
        }},
        ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreActionTag, info.Callback.Plugin);
        }
    }
}