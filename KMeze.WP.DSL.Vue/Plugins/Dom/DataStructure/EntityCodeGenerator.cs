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
        async post{ti.ToTitleCase(info.Name)}({info.Name}) {{
            let ret = null
            await axios.post(`${{this.apiUrl}}/wp-json/{info.Plugin.Slug}/v1/{info.Name}`, {info.Name}).then(res => {{
                ret = res.data
                this.{info.Name}.push(ret)
            }})

            return ret
        }},
        async put{ti.ToTitleCase(info.Name)}({info.Name}) {{
            let ret = null
            await axios.put(`${{this.apiUrl}}/wp-json/{info.Plugin.Slug}/v1/{info.Name}`, {info.Name}).then(res => {{
                ret = res.data
                this.{info.Name}.push(ret)
            }})

            return ret
        }},
        async delete{ti.ToTitleCase(info.Name)}(id) {{
            let ret = null
            await axios.delete(`${{this.apiUrl}}/wp-json/{info.Plugin.Slug}/v1/{info.Name}/${{id}}`).then(res => {{
                ret = res.data
                this.{info.Name} = this.{info.Name}.filter(e => e.id !== id)
            }})

            return ret
        }},
        ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreActionTag, info.Plugin);

            snippet = $@"this.{info.Name} = []
            ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreCleanUpActionTag, info.Plugin);
        }
    }
}