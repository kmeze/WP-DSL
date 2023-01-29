using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DbDeltaInfo))]
    public class DbDeltaCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DataStructureInfo> DbDeltaColumnTag = "DbDeltaColumn";
        public static readonly CsTag<DataStructureInfo> DbDeltaKeyTag = "DbDeltaKey";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DbDeltaInfo)conceptInfo;

            string snippet = $@"require_once( ABSPATH . 'wp-admin/includes/upgrade.php' );
    global $wpdb;
    $table_name = $wpdb->prefix . '{info.DataStructure.WPPlugin.Name}_{info.DataStructure.Name}';
    dbDelta( ""CREATE TABLE {{$table_name}} (
                        ID BIGINT(20) NOT NULL AUTO_INCREMENT
                        {DbDeltaColumnTag.Evaluate(info.DataStructure)}
                        ,PRIMARY KEY  (ID)
                        {DbDeltaKeyTag.Evaluate(info.DataStructure)}
                        ) {{$wpdb->get_charset_collate()}};"" );

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationHookTag, info.DataStructure.WPPlugin);
        }
    }
}