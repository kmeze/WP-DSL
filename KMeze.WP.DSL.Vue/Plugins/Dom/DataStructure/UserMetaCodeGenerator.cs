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

            // Generate user meta in state Me in Pinia store
            string snippet = $@",{info.Plugin.Slug}_{info.Name}: {{}}
                ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreMeFieldsTag, info.Plugin);

            // Generate user meta in state Me in /me api
            snippet = $@",'{info.Plugin.Slug}_{info.Name}'
                ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.PiniaStoreMeUrlFieldsTag, info.Plugin);
        }
    }
}