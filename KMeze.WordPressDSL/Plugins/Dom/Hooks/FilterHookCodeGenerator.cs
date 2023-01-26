using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(FilterHookInfo))]
    public class FilterHookCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (FilterHookInfo)conceptInfo;

            var priority = info.Priority;
            if (priority.Trim().ToLower() == "defaultpriority") priority = "10";

            var args = info.Args;
            var acceptedArgs = args.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            string snippet = $@"add_filter( '{info.HookName}', '{info.Callback.WPPlugin.Name}_{info.Callback.Name}', {priority}, {acceptedArgs.Count()});

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);

            snippet = $@"{String.Join(", ", acceptedArgs)}";
            codeBuilder.InsertCode(snippet, ActionCodeGenerator.ActionArgTag, info.Callback);
        }
    }
}