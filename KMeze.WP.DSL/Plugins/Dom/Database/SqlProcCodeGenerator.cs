using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(SqlProcInfo))]
    public class SqlProcCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (SqlProcInfo)conceptInfo;

            // create procedure
            string snippet = $@"global $wpdb;
    $dbName = DB_NAME;
    $procName = ""$dbName.{info.Plugin.Slug}_{info.Name}"";    
    $sql = ""CREATE PROCEDURE IF NOT EXISTS $procName({info.Args})
    BEGIN
        DECLARE EXIT HANDLER FOR SQLEXCEPTION 
        BEGIN 
	        ROLLBACK;
	        RESIGNAL;
        END;
    
    	START TRANSACTION;
            {info.Script}    	
        COMMIT;
    END"";
    $res = $wpdb->query($sql);
    if ( ! (bool) $res ) throw new Exception( $wpdb->last_error );
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActivationAfterDbDeltaHookTag, info.Plugin);

            // drop procedure
            snippet = $@"global $wpdb;
    $dbName = DB_NAME;
    $procName = ""$dbName.{info.Plugin.Slug}_{info.Name}"";    
    $sql = ""DROP PROCEDURE IF EXISTS $procName"";
    $res = $wpdb->query($sql);
    if ( ! (bool) $res ) throw new Exception( $wpdb->last_error );
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DeactivationHookTag, info.Plugin);
        }
    }
}