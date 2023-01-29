using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(RepositoryInfo))]
    public class RepositoryCodeGenerator : IWPPluginConceptCodeGenerator
    {

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (RepositoryInfo)conceptInfo;

            string snippet = $@"class {info.DataStructure.WPPlugin.Name}_{info.DataStructure.Name}_Repository {{
    public function select_{info.DataStructure.Name}() {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.DataStructure.WPPlugin.Name}_{info.DataStructure.Name}';

        return $wpdb->get_results( ""SELECT * FROM {{$table_name}};"" );
    }}

    public function select_{info.DataStructure.Name}_by_ID( int $id ) {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.DataStructure.WPPlugin.Name}_{info.DataStructure.Name}';

        return $wpdb->get_row( ""SELECT * FROM {{$table_name}} WHERE ID={{$id}};"" );
    }}

    public function insert_{info.DataStructure.Name}( array $data ) {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.DataStructure.WPPlugin.Name}_{info.DataStructure.Name}';
        $wpdb->insert( $table_name, $data );

        return $wpdb->insert_id;
    }}

    public function update_{info.DataStructure.Name}( int $id, array $data ) {{
	    global $wpdb;
	    $table_name = $wpdb->prefix . '{info.DataStructure.WPPlugin.Name}_{info.DataStructure.Name}';
	    $wpdb->update( $table_name, $data, array( 'id' => $id ) );
    }}

    public function delete_{info.DataStructure.Name}( int $id ) {{
	    global $wpdb;
	    $table_name = $wpdb->prefix . '{info.DataStructure.WPPlugin.Name}_{info.DataStructure.Name}';
	    $wpdb->delete( $table_name, array( 'id' => $id ) );
    }}
}}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.DataStructure.WPPlugin);
        }
    }
}