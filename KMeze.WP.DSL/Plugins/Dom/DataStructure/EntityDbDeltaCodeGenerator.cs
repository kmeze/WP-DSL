using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(EntityInfo))]
    public class EntityDbDeltaCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<DataStructureInfo> DbDeltaColumnTag = "DbDeltaColumn";
        public static readonly CsTag<DataStructureInfo> DbDeltaKeyTag = "DbDeltaKey";
        public static readonly CsTag<DataStructureInfo> DbDeltaAfterTag = "DbDeltaAfter";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            string snippet = $@"require_once( ABSPATH . 'wp-admin/includes/upgrade.php' );
    global $wpdb;
    $table_name = $wpdb->prefix . '{info.Plugin.Slug}_{info.Name}';
    dbDelta( ""CREATE TABLE {{$table_name}} (
                        ID BIGINT(20) UNSIGNED NOT NULL AUTO_INCREMENT
                        {DbDeltaColumnTag.Evaluate(info)}
                        ,PRIMARY KEY  (ID)
                        {DbDeltaKeyTag.Evaluate(info)}
                        ) {{$wpdb->get_charset_collate()}};"" );

    {DbDeltaAfterTag.Evaluate(info)}

    ";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationDbDeltaHookTag, info.Plugin);
        }
    }
}