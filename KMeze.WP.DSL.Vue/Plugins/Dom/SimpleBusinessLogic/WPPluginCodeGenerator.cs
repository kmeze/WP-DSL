using System;
using System.ComponentModel.Composition;
using System.Globalization;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL.Vue.Pinia
{
    [Export(typeof(IWPPluginJsSdkConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(WPPluginInfo))]
    public class WPPluginCodeGenerator : IWPPluginJsSdkConceptCodeGenerator
    {
        public static readonly CsTag<WPPluginInfo> PiniaStoreStateTag = "PiniaStoreState";
        public static readonly CsTag<WPPluginInfo> PiniaStoreGettersTag = "PiniaStoreGetters";
        public static readonly CsTag<WPPluginInfo> PiniaStoreActionTag = "PiniaStoreAction";
        public static readonly CsTag<WPPluginInfo> PiniaStoreMeFieldsTag = "PiniaStoreMeFields";
        public static readonly CsTag<WPPluginInfo> PiniaStoreMeUrlFieldsTag = "PiniaStoreMeUrlFields";
        public static readonly CsTag<WPPluginInfo> PiniaStoreCleanUpActionTag = "PiniaStoreActionCleanUp";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (WPPluginInfo)conceptInfo;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            codeBuilder.InsertCodeToFile(
$@"import {{defineStore}} from '@/../node_modules/pinia'
import axios from '@/../node_modules/axios'

export const use{ti.ToTitleCase(info.Slug)}Store = defineStore('{info.Slug}', {{
    state: () => {{
        return {{
            apiUrl: '',
            isLoggedIn: false,
            me: {{
                id: null
                ,username: null
                {PiniaStoreMeFieldsTag.Evaluate(info)}
            }},
            {PiniaStoreStateTag.Evaluate(info)}
        }}
    }},
    getters: {{
        {PiniaStoreGettersTag.Evaluate(info)}
    }},
    actions: {{
        {PiniaStoreActionTag.Evaluate(info)}
        async getMe() {{
            const fields = [
                'id'
                ,'username'
                {PiniaStoreMeUrlFieldsTag.Evaluate(info)}
            ]

            const queryString = fields.join(',')
            await axios.get(`${{this.apiUrl}}/wp-json/wp/v2/users/me?context=edit&_fields=${{queryString}}`).then(res => {{
                this.me = res.data
            }})

            return this.me
        }},
        async postMe() {{
            const fields = [
                'id'
                ,'username'
                {PiniaStoreMeUrlFieldsTag.Evaluate(info)}
            ]

            const queryString = fields.join(',')
            await axios.post(`${{this.apiUrl}}/wp-json/wp/v2/users/me?context=edit&_fields=${{queryString}}`, this.me).then(res => {{
                this.me = res.data
            }})

            return this.me
        }},
        async cleanUp() {{
            {PiniaStoreCleanUpActionTag.Evaluate(info)}
        }},
    }},
}})", $"{Path.Combine(Path.Combine(Path.Combine(Path.Combine("WordPress", info.Slug), "src"), "stores"), info.Slug + "Store")}");
        }
    }
}