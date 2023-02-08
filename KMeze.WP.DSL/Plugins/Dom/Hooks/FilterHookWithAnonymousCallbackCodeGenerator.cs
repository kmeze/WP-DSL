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

            string snippet = $@"add_filter( '{info.Hook}', {info.Script}, {priority}, {info.AcceptedArgs});
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.FilterHooksTag, info.WPPlugin);
        }
    }
}