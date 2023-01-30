using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ListInfo))]
    public class ListCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<ListInfo> ListColumnTag = "ListColumn";
        public static readonly CsTag<ListInfo> ListJoinTag = "ListJoin";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ListInfo)conceptInfo;

            string snippet = $@"

$slq = "" SELECT cvis_{info.Source.WPPlugin.Name}_{info.Source.Name}.ID
    {ListColumnTag.Evaluate(info)}
FROM cvis_{info.Source.WPPlugin.Name}_{info.Source.Name}
{ListJoinTag.Evaluate(info)}; "";

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);
        }
    }
}