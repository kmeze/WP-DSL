using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ActionInfo))]
    public class ActionCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<ActionInfo> ActionArgTag = "ActionArg";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ActionInfo)conceptInfo;

            string snippet = $@"function {info.WPPlugin.Name}_{info.Name} ( {ActionArgTag.Evaluate(info)} ) {{
    {info.Script}
}}
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.CallbacksTag, info.WPPlugin);
        }
    }
}