using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL.Vue.Pinia
{
    [Export(typeof(IWPPluginJsSdkConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(UserMetaInfo))]
    public class UserMetaCodeGenerator : IWPPluginJsSdkConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (UserMetaInfo)conceptInfo;

            string snippet = $@"async fetch{info.Plugin.Slug}_{info.Name}() {{
            let {info.Plugin.Slug}_{info.Name} = null;
            await axios.get(`${{this.apiUrl}}/wp-json/wp/v2/users/me?context=edit&_fields={info.Plugin.Slug}_{info.Name}`).then(res => {{
                {info.Plugin.Slug}_{info.Name} = res.data.{info.Plugin.Slug}_{info.Name}
                this.me.{info.Plugin.Slug}_{info.Name} = {info.Plugin.Slug}_{info.Name}
            }})

            return {info.Plugin.Slug}_{info.Name}
        }},
        ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreActionTag, info.Plugin);

            snippet = $@"await this.fetch{info.Plugin.Slug}_{info.Name}()";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreFetchMeTag, info.Plugin);
        }
    }
}