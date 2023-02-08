using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(FilterHookWithAnonymousCallbackInfo))]
    public class FilterHookWithAnonymousCallbackCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (FilterHookWithAnonymousCallbackInfo)conceptInfo;

            var priority = info.Priority;
            if (priority.Trim().ToLower() == "defaultpriority") priority = "10";

            var args = info.Args;
            var acceptedArgs = args.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            string snippet = $@"add_filter( '{info.Hook}', {info.Script}, {priority}, {acceptedArgs.Count()});
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.FilterHooksTag, info.WPPlugin);
        }
    }
}