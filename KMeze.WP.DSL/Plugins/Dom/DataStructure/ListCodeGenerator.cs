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

            string snippet = $@"public ?int $id = null;
    ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.DataStructureClassPropertyTag, info);

            snippet = $@"$dataStructure->id = (int) $object->ID;
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.DataClassParsePropertyTag, info);

            snippet = $@"protected ?string $source_table_name = null;
    ";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassPropertyTag, info);

            snippet = $@"$this->source_table_name = $this->wpdb->prefix . '{info.Source.WPPlugin.Name}_{info.Source.Name}';
        ";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassConstructorTag, info);

            snippet = $@"public function get() {{

        $sql = ""SELECT $this->source_table_name.ID
                    {ListColumnTag.Evaluate(info)}
                FROM $this->source_table_name
                {ListJoinTag.Evaluate(info)};"";

        return $this->wpdb->get_results( $sql );
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassMethodTag, info);

            snippet = $@"register_rest_route( $this->namespace, $this->resource_name, array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_items' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        ";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassRegisterRoutesTag, info);

            snippet = $@"private function prepare_item_for_response( $row ) {{
        return {info.WPPlugin.Name}_{info.Name}::parse( $row );
    }}

    public function get_items( $request ): array {{
	    return array_map( function ( $row ) {{
	        return $this->prepare_item_for_response( $row );
	    }}, ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->get() );
    }}

";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassMethodTag, info);
        }
    }
}