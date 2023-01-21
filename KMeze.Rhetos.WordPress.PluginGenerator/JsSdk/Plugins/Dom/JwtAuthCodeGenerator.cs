using System;
using System.ComponentModel.Composition;
using Newtonsoft.Json.Linq;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.Rhetos.WordPress.PluginGenerator.JsSdk
{
    [Export(typeof(IWPPluginJsSdkConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(JwtAuthInfo))]
    public class JwtAuthCodeGenerator : IWPPluginJsSdkConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (JwtAuthInfo)conceptInfo;

            // Generate entity state in Pinia store
            string snippet = $@"authToken: '',
            ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreStateTag, info.WPPlugin);

            // Generate getters in Pinia store
            snippet = $@"jwtIsLoggedIn(state) {{
            if (state.authToken) return true
            if (localStorage.token) {{
                state.authToken = localStorage.token;
                return true
            }}

            return false
        }},
        ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreGettersTag, info.WPPlugin);

            // Generate entity actions in Pinia store
            snippet = $@"jwtLogIn(username, password, rememberMe = false) {{
            let token = ''
            let authorization = ''
            const res = axios.post(`${{this.apiUrl}}/wp-json/jwt-auth/v1/token`, {{
                ""username"": username,
                ""password"": password,
            }}).then(res => {{
                token = res.data.token
                authorization = `Bearer ${{token}}`
                if(rememberMe) localStorage.token = JSON.stringify(token)
            }}).catch((error) => {{
                localStorage.removeItem(""token"")
                this.cleanUp()
                console.log('%cError: %s', 'color: red;', error.message)
            }}).finally(() => {{
                this.authToken = token
                axios.defaults.headers['Authorization'] = authorization
            }})
        }},
        jwtLogOut() {{
            this.authToken = ''
            axios.defaults.headers['Authorization'] = ``
            localStorage.removeItem(""token"")
            this.cleanUp()
        }},
        ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreActionTag, info.WPPlugin);
        }
    }
}