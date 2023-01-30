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
    public class EntityRepositoryCodeGenerator : IWPPluginConceptCodeGenerator
    {

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            string snippet = $@"class {info.WPPlugin.Name}_{info.Name}_Repository {{
    public function select_{info.Name}() {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';

        return $wpdb->get_results( ""SELECT * FROM {{$table_name}};"" );
    }}

    public function select_{info.Name}_by_ID( int $id ) {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';

        return $wpdb->get_row( ""SELECT * FROM {{$table_name}} WHERE ID={{$id}};"" );
    }}

    public function insert_{info.Name}( array $data ) {{
        global $wpdb;
        $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
        $wpdb->insert( $table_name, $data );

        return $wpdb->insert_id;
    }}

    public function update_{info.Name}( int $id, array $data ) {{
	    global $wpdb;
	    $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
	    $wpdb->update( $table_name, $data, array( 'id' => $id ) );
    }}

    public function delete_{info.Name}( int $id ) {{
	    global $wpdb;
	    $table_name = $wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
	    $wpdb->delete( $table_name, array( 'id' => $id ) );
    }}
}}

";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.BodyTag, info.WPPlugin);
        }
    }
}