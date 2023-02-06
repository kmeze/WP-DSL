using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(ReferencePropertyInfo))]
    public class ReferencePropertyCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ReferencePropertyInfo)conceptInfo;

            string snippet = $@"public ?int ${info.Name}_id = null;
    ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassPropertyTag, info.DataStructure);

            snippet = $@"$dataStructure->{info.Name}_id = is_null($object->{info.Name}_id) ? null : (int) $object->{info.Name}_id;
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.DataClassParsePropertyTag, info.DataStructure);

            if (info.DataStructure is EntityInfo)
            {
                snippet = $@",{info.Name}_id BIGINT(20) UNSIGNED";
                codeBuilder.InsertCode(snippet, PropertyCodeGenerator.DbDeltaPropertyColumnTag, info);

                snippet = $@",KEY ind_{info.DataStructure.Name}_{info.Name}_id ({info.Name}_id)
                        ";
                codeBuilder.InsertCode(snippet, EntityDbDeltaCodeGenerator.DbDeltaKeyTag, info.DataStructure);

                // Fast hack to enable adding reference to wp_users table
                string referencedTable = $@"{info.ReferencedDataStructure.WPPlugin.Name}_{info.ReferencedDataStructure.Name}";
                if (info.ReferencedDataStructure is WPDataStructureInfo && info.ReferencedDataStructure.Name == "User") referencedTable = "users";

                snippet = $@"$referenced_table_name = $wpdb->prefix . '{referencedTable}';
    $db_name               = DB_NAME;
	$key_name              = ""fk_{info.DataStructure.Name}_{info.Name}_id"";
	$sql                   = ""SELECT CONSTRAINT_NAME FROM information_schema.TABLE_CONSTRAINTS WHERE CONSTRAINT_SCHEMA = '$db_name' AND CONSTRAINT_NAME = '$key_name' AND CONSTRAINT_TYPE = 'FOREIGN KEY';"";

	if ( is_null( $wpdb->get_var( $sql ) ) ) {{
		$res = $wpdb->query( ""ALTER TABLE $table_name ADD CONSTRAINT $key_name FOREIGN KEY ({info.Name}_id) REFERENCES $referenced_table_name (ID) ON DELETE CASCADE ON UPDATE CASCADE;"" );

		if ( ! (bool) $res ) throw new Exception( $wpdb->last_error );
	}}

    ";
                codeBuilder.InsertCode(snippet, EntityDbDeltaCodeGenerator.DbDeltaAfterTag, info.DataStructure);
            }
        }
    }
}