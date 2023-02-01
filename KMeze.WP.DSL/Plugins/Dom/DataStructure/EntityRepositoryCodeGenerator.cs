﻿using System;
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

            string snippet = $@"protected ?string ${info.Name}_table_name = null;
    ";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassPropertyTag, info);

            snippet = $@"$this->{info.Name}_table_name = $this->wpdb->prefix . '{info.WPPlugin.Name}_{info.Name}';
        ";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassConstructorTag, info);

            snippet = $@"public function get() {{
        return $this->wpdb->get_results( ""SELECT * FROM $this->{info.Name}_table_name;"" );
    }}

    public function get_by_ID( int $id ) {{
        return $this->wpdb->get_row( ""SELECT * FROM $this->{info.Name}_table_name WHERE ID=$id;"" );
    }}

    public function insert( array $data ): int {{
        $this->wpdb->insert( $this->{info.Name}_table_name, $data );

        return $this->insert_id;
    }}

    public function update( int $id, array $data ) {{
	    $this->wpdb->update( $this->{info.Name}_table_name, $data, array( 'ID' => $id ) );
    }}

    public function delete( int $id ) {{
	    $this->wpdb->delete( $this->{info.Name}_table_name, array( 'ID' => $id ) );
    }}

";
            codeBuilder.InsertCode(snippet, RepositoryCodeGenerator.RepositoryClassMethodTag, info);
        }
    }
}