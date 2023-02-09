using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(CallbackInfo))]
    public class CallbackCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<CallbackInfo> CallbackArgTag = "CallbackArg";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (CallbackInfo)conceptInfo;

            string snippet = $@"function {info.Plugin.Slug}_{info.Name} ( {CallbackArgTag.Evaluate(info)} ) {{
    {info.Script}
}}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.CallbacksTag, info.Plugin);
        }
    }
}