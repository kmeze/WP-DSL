# Rhetos WordPress Plugin Generator

Rhetos WordPress Plugin Generator simplifies WordPress plugin development by generating PHP file from [Rhetos DSL](https://github.com/Rhetos/Rhetos/wiki/What-is-Rhetos#rhetos-dsl) script. It is a plugin package for [Rhetos development platform](https://github.com/Rhetos/Rhetos).

See [rhetos.org](http://www.rhetos.org/) for more information on Rhetos.

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
// Let's start by creating MyPlugin.php file using WPPlugin concept
WPPlugin MyPlugin
{
  // Create Non-Post Type entity (PHP class, database table, REST endpoints)
  Entity
  {
    // Set some entity properties and generates columns in database table
    ShortString Title;
    Int         Count;
  
    // Set permission check callback for REST endpoints. Callback code is set in Action concept bellow.
    Authorization: AllowPublic;
  
    // Drop custom table from database on plugin activation. Delete or comment line below to preserve table.
    DropOnDeactivate;
  }
    
  // The Action concept bellow creates function AllowPublic() in main plugin file that is used for permission check callback by REST endpoints.
  Action AllowPublic 'return true;'
}
```
