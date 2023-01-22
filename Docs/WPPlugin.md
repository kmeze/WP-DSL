# WPPlugin concept

## Description

Generates plugin folder and main plugin file with required Plugin Name header comment and activation/deactivation and uninstall hooks.

## Usage

```
WPPlugin <plugin_name>;
```

### Note(s)

* DSL script (.rhe file) must be in **DslScripts** project suborder (***project***/DslScripts)
* plugin folder is generated in ***project***/obj/Rhetos/Source/WordPress/***plugin_name*** folder

## Example

```
// HelloWorld.rhe

WPPlugin HelloWorld;
```

### Generated code

```php
<?php
/**
 * Plugin Name: HelloWorld
 */

// Exit if accessed directly
defined( 'ABSPATH' ) || exit;

/*WPPluginInfo PluginBody HelloWorld*/

register_activation_hook( __FILE__, function () {
    /*WPPluginInfo ActivationHook HelloWorld*/
} );

register_deactivation_hook( __FILE__, function () {
    /*WPPluginInfo DeactivationHook HelloWorld*/
} );

// NOTE: register_uninstall_hook callback cannot be anonymous function
register_uninstall_hook( __FILE__, 'HelloWorld_uninstall' );

function HelloWorld_uninstall() {
    /*WPPluginInfo UninstallHook HelloWorld*/
}
```
