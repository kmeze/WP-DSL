using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.Rhetos.WordPress.PluginGenerator.JsSdk
{
    [Export(typeof(IWPPluginJsSdkConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(WPPluginInfo))]
    public class WPPluginCodeGenerator : IWPPluginJsSdkConceptCodeGenerator
    {
        public static readonly CsTag<WPPluginInfo> PiniaStoreStateTag = "PiniaStoreState";
        public static readonly CsTag<WPPluginInfo> PiniaStoreGettersTag = "PiniaStoreGetters";
        public static readonly CsTag<WPPluginInfo> PiniaStoreActionTag = "PiniaStoreAction";
        public static readonly CsTag<WPPluginInfo> PiniaStoreCleanUpActionTag = "PiniaStoreActionCleanUp";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (WPPluginInfo)conceptInfo;

            codeBuilder.InsertCodeToFile(
$@"import {{ defineStore }} from 'pinia'
import axios from 'axios'

export const use{info.Name}Store = defineStore('{info.Name}', {{
    state: () => {{
        return {{
            apiUrl: '',
            {PiniaStoreStateTag.Evaluate(info)}
        }}
    }},
    getters: {{
    {PiniaStoreGettersTag.Evaluate(info)}
    }},
    actions: {{
        {PiniaStoreActionTag.Evaluate(info)}
        cleanUp() {{
            {PiniaStoreCleanUpActionTag.Evaluate(info)}
        }},
    }},
}})", $"{Path.Combine(Path.Combine(Path.Combine(Path.Combine("WordPress", info.Name), "src"), "stores"), info.Name + "Store")}");
        }
    }
}