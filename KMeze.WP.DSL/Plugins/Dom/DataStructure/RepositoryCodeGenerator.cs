using System;
using System.ComponentModel.Composition;
using Newtonsoft.Json.Linq;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DataStructureInfo))]
    public class RepositoryCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DataStructureInfo> RepositoryClassPropertyTag = "RepositoryClassProperty";
        public static readonly CsTag<DataStructureInfo> RepositoryClassConstructorTag = "RepositoryClassConstructor";
        public static readonly CsTag<DataStructureInfo> RepositoryClassMethodTag = "RepositoryClassMethod";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;

            string snippet = $@"class {info.WPPlugin.Name}_{info.Name}_Repository {{
    protected ?wpdb $wpdb = null;
    {RepositoryClassPropertyTag.Evaluate(info)}

    public function __construct() {{
        global $wpdb;
        $this->wpdb = $wpdb;
        {RepositoryClassConstructorTag.Evaluate(info)}
    }}

    {RepositoryClassMethodTag.Evaluate(info)}
}}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);
        }
    }
}