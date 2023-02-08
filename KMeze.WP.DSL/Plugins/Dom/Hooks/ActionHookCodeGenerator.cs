using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ActionHookInfo))]
    public class ActionHookCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<CallbackInfo> ActionHookPriorityTag = "ActionHookPriority";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ActionHookInfo)conceptInfo;

            var priority = info.Priority;
            if (priority.Trim().ToLower() == "defaultpriority") priority = "10";

            var args = info.Args;
            var acceptedArgs = args.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            string snippet = $@"add_action( '{info.Hook}', '{info.Callback.WPPlugin.Name}_{info.Callback.Name}', {priority}, {acceptedArgs.Count()});
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActionHooksTag, info.WPPlugin);

            snippet = $@"{String.Join(", ", acceptedArgs)}";
            codeBuilder.InsertCode(snippet, CallbackCodeGenerator.CallbackArgTag, info.Callback);
        }
    }
}