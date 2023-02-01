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
    public class EntityRestControllerCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<EntityInfo> AuthorizationTag = "Authorization";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EntityInfo)conceptInfo;

            string snippet = $@"register_rest_route( $this->namespace, $this->resource_name, array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_items' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        register_rest_route( $this->namespace, $this->resource_name . '/(?P<id>[\d]+)', array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_item' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        register_rest_route( $this->namespace, $this->resource_name, array(
            'methods'             => 'POST',
            'callback'            => array( $this, 'create_item' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        register_rest_route( $this->namespace, $this->resource_name . '/(?P<id>[\d]+)', array(
            'methods'             => 'PUT',
            'callback'            => array( $this, 'update_item' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );

        register_rest_route( $this->namespace, $this->resource_name . '/(?P<id>[\d]+)', array(
            'methods'             => 'DELETE',
            'callback'            => array( $this, 'delete_item' ),
            'permission_callback' => array( $this, 'item_permissions_check' ),
        ) );
        ";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassRegisterRoutesTag, info);

            snippet = $@"public function item_permissions_check( $request ): bool {{
        {AuthorizationTag.Evaluate(info)}

        return false;
    }}

    private function prepare_item_for_response( $row ) {{
        return {info.WPPlugin.Name}_{info.Name}::parse( $row );
    }}

    public function get_items( $request ): array {{
	    return array_map( function ( $row ) {{
	        return $this->prepare_item_for_response( $row );
	    }}, ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->get() );
    }}

    public function get_item( $request ) {{
        return $this->prepare_item_for_response( ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->get_by_ID( $request->get_param( 'id' ) ) );
    }}

    public function create_item( $request ) {{
        return ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->insert( $request->get_json_params() );
    }}

    public function update_item( $request ) {{
        return ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->update( $request->get_param( 'id' ), $request->get_json_params() );
    }}

    public function delete_item( $request ) {{
        return ( new {info.WPPlugin.Name}_{info.Name}_Repository() )->delete( $request->get_param( 'id' ) );
    }}

";
            codeBuilder.InsertCode(snippet, RestControllerCodeGenerator.RestControllerClassMethodTag, info);
        }
    }
}