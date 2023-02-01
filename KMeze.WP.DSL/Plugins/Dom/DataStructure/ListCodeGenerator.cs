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

            string snippet = $@"public function get() {{

        // TODO: IMPORTANT CVIS_ MUST BE wpdb->prefilx; MULTIPLE CS FILES
        $sql = ""SELECT cvis_{info.Source.WPPlugin.Name}_{info.Source.Name}.ID
                    {ListColumnTag.Evaluate(info)}
                FROM cvis_{info.Source.WPPlugin.Name}_{info.Source.Name}
                {ListJoinTag.Evaluate(info)};"";

        return $this->wpdb->get_results( $sql );
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassMethodTag, info);
        }
    }
}