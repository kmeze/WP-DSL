using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ActionHookInfo))]
    public class ActionHookCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ActionHookInfo)conceptInfo;

            var args = info.Args;
            var acceptedArgs = args.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            string snippet = $@"add_action( '{info.HookName}', '{info.Callback.WPPlugin.Name}_{info.Callback.Name}', {info.Priority}, {acceptedArgs.Count()});

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);

            snippet = $@"{String.Join(", ", acceptedArgs)}";
            codeBuilder.InsertCode(snippet, ActionCodeGenerator.ActionArgTag, info.Callback);
        }
    }
}