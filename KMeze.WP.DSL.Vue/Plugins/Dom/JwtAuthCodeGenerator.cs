using System;
using System.ComponentModel.Composition;
using Newtonsoft.Json.Linq;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL.Vue.Pinia
{
    [Export(typeof(IWPPluginJsSdkConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(JwtAuthInfo))]
    public class JwtAuthCodeGenerator : IWPPluginJsSdkConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (JwtAuthInfo)conceptInfo;

            // Generate state in Pinia store
            string snippet = $@"jwtAuthToken: '',
            ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreStateTag, info.WPPlugin);

            // Generate actions in Pinia store
            snippet = $@"async tryToLogInUser() {{
            if (!localStorage.token) return false

            const token = localStorage.token
            await axios.post(`${{this.apiUrl}}/wp-json/jwt-auth/v1/token/validate`, null, {{
                headers: {{Authorization: `Bearer ${{token}}`}}
            }}).then(res => {{
                this.jwtAuthToken = token
                this.isLoggedIn = true
                axios.defaults.headers['Authorization'] = `Bearer ${{token}}`
            }}).catch((error) => {{
            }})

            return this.isLoggedIn
        }},
        async jwtLogIn(username, password, rememberMe = false) {{
            let token = ''
            let authorization = ''
            let loggedIn = false
            await axios.post(`${{this.apiUrl}}/wp-json/jwt-auth/v1/token`, {{
                ""username"": username,
                ""password"": password,
            }}).then(res => {{
                token = res.data.token
                loggedIn = true
                authorization = `Bearer ${{token}}`
                if (rememberMe) localStorage.token = token
            }}).catch((error) => {{
                localStorage.removeItem(""token"")
                this.cleanUp()
            }}).finally(() => {{
                this.jwtAuthToken = token
                this.isLoggedIn = loggedIn
                axios.defaults.headers['Authorization'] = authorization
            }})

            return this.isLoggedIn
        }},
        async jwtLogOut() {{
            this.jwtAuthToken = ''
            this.isLoggedIn = false
            axios.defaults.headers['Authorization'] = ``
            localStorage.removeItem(""token"")
            await this.cleanUp()
        }},
        ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreActionTag, info.WPPlugin);
        }
    }
}