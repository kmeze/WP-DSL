# WordPressDSL

WordPressDSL is the implementation of WordPress [domain-specific language](https://en.wikipedia.org/wiki/Domain-specific_language) (DSL). It simplifies WordPress plugin development in the way that developers (mostly) use a specific high-level programming language to create WordPress plugins instead of writing extensive PHP code.

WordPressDSL is a plugin package
for [Rhetos platform](https://github.com/Rhetos/Rhetos). See [rhetos.org](http://www.rhetos.org/)
and [Rhetos wiki](https://github.com/Rhetos/Rhetos/wiki) for more information on Rhetos.

See [WordPress Plugin Handbook](https://developer.wordpress.org/plugins/) for more information on WordPress plugin
development.

## Features

* generates WordPress main plugin file with
  required [header comment](https://developer.wordpress.org/plugins/plugin-basics/header-requirements/)
* setups
  plugin [Activation / Deactivation / Uninstall Hooks](https://developer.wordpress.org/plugins/plugin-basics/activation-deactivation-hooks/)
* simplifies working with custom Non-Post Type entities:
    * generates PHP classes for custom entities
    * generates [dbDelta function](https://developer.wordpress.org/reference/functions/dbdelta/) calls for create and
      update tables in the database
    * generates [REST API](https://developer.wordpress.org/rest-api/) endpoints and controller classes
    * generates database clean-up code

### Simple Example

Following is DSL script example that demos some features.

```
WPPlugin MyPlugin
{
  Entity TestEntity
  {
    ShortString Title;
    Integer     Count;
  
    Authorization MyPlugin.AllowPublic;
  
    DropOnDeactivate;
  }
    
  Action AllowPublic 'return true;';
}
```

Some script explanations for beginning:

1. We started with `WPPlugin` keyword to generate MyPlugin.php file.
2. `Entity` keyword will create Non-Post Type entity (PHP class, database table, REST endpoints) together with its
   properties (using ShortString and Integer keywords).
3. In `Authorization` keyword we set permission check callback for REST endpoints. Callback code is set in Action
   keyword bellow.
4. `DropOnDeactivate` drops custom table from database on plugin deactivation. Delete or comment that line preserve
   table.
5. The `Action` keyword creates `MyPlugin_AllowPublic() { return true; };` function in main plugin file that is used for
   permission check callback by REST endpoints.

After running build following PHP code is created. And it is quite of code for simple script above that doesn't have to
be written (and debugged) manually;

```php
<?php
/**
 * Plugin Name: MyPlugin
 */

// Exit if accessed directly
defined( 'ABSPATH' ) || exit;

function MyPlugin_AllowPublic() {
    return true;
}

class MyPlugin_TestEntity {
    public ?int $id = null;
    public ?int $Count = null;
    public ?string $Title = null;
    /*EntityInfo ClassProperty MyPlugin.TestEntity*/
}

class MyPlugin_TestEntity_Repository {
    public function select_TestEntity() {
        global $wpdb;
        $table_name = $wpdb->prefix . 'MyPlugin_TestEntity';

        return $wpdb->get_results( "SELECT * FROM {$table_name};" );
    }

    public function select_TestEntity_by_ID( int $id ) {
        global $wpdb;
        $table_name = $wpdb->prefix . 'MyPlugin_TestEntity';

        return $wpdb->get_row( "SELECT * FROM {$table_name} WHERE ID={$id};" );
    }

    public function insert_TestEntity( array $data ) {
        global $wpdb;
        $table_name = $wpdb->prefix . 'MyPlugin_TestEntity';
        $wpdb->insert( $table_name, $data );

        return $wpdb->insert_id;
    }

    public function update_TestEntity( int $id, array $data ) {
	    global $wpdb;
	    $table_name = $wpdb->prefix . 'MyPlugin_TestEntity';
	    $wpdb->update( $table_name, $data, array( 'id' => $id ) );
    }

    public function delete_TestEntity( int $id ) {
	    global $wpdb;
	    $table_name = $wpdb->prefix . 'MyPlugin_TestEntity';
	    $wpdb->delete( $table_name, array( 'id' => $id ) );
    }
}

class MyPlugin_TestEntity_REST_Controller {
    public function register_routes() {
        register_rest_route( 'MyPlugin/v1', '/TestEntity', array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_items' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );

        register_rest_route( 'MyPlugin/v1', '/TestEntity/(?P<id>[\d]+)', array(
            'methods'             => 'GET',
            'callback'            => array( $this, 'get_item' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );

        register_rest_route( 'MyPlugin/v1', '/TestEntity', array(
            'methods'             => 'POST',
            'callback'            => array( $this, 'post_item' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );

        register_rest_route( 'MyPlugin/v1', '/TestEntity/(?P<id>[\d]+)', array(
            'methods'             => 'PUT',
            'callback'            => array( $this, 'put_item' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );

        register_rest_route( 'MyPlugin/v1', '/TestEntity/(?P<id>[\d]+)', array(
            'methods'             => 'DELETE',
            'callback'            => array( $this, 'delete_item' ),
            'permission_callback' => array( $this, 'permissions_check' ),
        ) );
    }

    public function permissions_check( $request ): bool {
        if ( (bool) MyPlugin_AllowPublic () ) {return true;}

        /*EntityInfo Authorization MyPlugin.TestEntity*/

        return false;
    }

    private function prepare_item_for_response( $row ) {
        $entity     = new MyPlugin_TestEntity();
        $entity->id = (int) $row->ID;
        $entity->Count = is_null($row->Count) ? null : (int) $row->Count;
        $entity->Title = $row->Title;
        /*EntityInfo ColumnMap MyPlugin.TestEntity*/

        return $entity;
    }

    public function get_items( $request ): array {
	    return array_map( function ( $row ) {
	        return $this->prepare_item_for_response( $row );
	    }, ( new MyPlugin_TestEntity_Repository() )->select_TestEntity() );
    }

    public function get_item( $request ) {
        return $this->prepare_item_for_response( ( new MyPlugin_TestEntity_Repository() )->select_TestEntity_by_ID( $request->get_param( 'id' ) ) );
    }

    public function post_item( $request ) {
        return ( new MyPlugin_TestEntity_Repository() )->insert_TestEntity( $request->get_json_params() );
    }

    public function put_item( $request ) {
        return ( new MyPlugin_TestEntity_Repository() )->update_TestEntity( $request->get_param( 'id' ), $request->get_json_params() );
    }

    public function delete_item( $request ) {
        return ( new MyPlugin_TestEntity_Repository() )->delete_TestEntity( $request->get_param( 'id' ) );
    }
}

add_action( 'rest_api_init', function () {
    $controller = new MyPlugin_TestEntity_REST_Controller();
    $controller->register_routes();
} );

/*WPPluginInfo PluginBody MyPlugin*/

register_activation_hook( __FILE__, function () {
    require_once( ABSPATH . 'wp-admin/includes/upgrade.php' );
    global $wpdb;
    $table_name = $wpdb->prefix . 'MyPlugin_TestEntity';
    dbDelta( "CREATE TABLE {$table_name} (
                        ID BIGINT(20) NOT NULL AUTO_INCREMENT
                        ,Count INTEGER
                        ,Title VARCHAR(256)
                        /*EntityInfo Column MyPlugin.TestEntity*/
                        ,PRIMARY KEY  (ID)
                        /*EntityInfo KeyMap MyPlugin.TestEntity*/
                        ) {$wpdb->get_charset_collate()};" );

    /*WPPluginInfo ActivationHook MyPlugin*/
} );

register_deactivation_hook( __FILE__, function () {
    global $wpdb;
    $table_name = $wpdb->prefix . 'MyPlugin_TestEntity';
    $wpdb->query( "DROP TABLE IF EXISTS {$table_name}" );

    /*WPPluginInfo DeactivationHook MyPlugin*/
} );

// NOTE: register_uninstall_hook callback cannot be anonymous function
register_uninstall_hook( __FILE__, 'MyPlugin_uninstall' );

function MyPlugin_uninstall() {
    /*WPPluginInfo UninstallHook MyPlugin*/
}
```
