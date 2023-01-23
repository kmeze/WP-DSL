# ActivationAction / DeactivationAction / UninstallAction concepts

## Description
Call function generated with [Action](Action.md) concept to the plugin activation / deactivation / uninstall hook.

## Usage

```
ActivationAction <Action>;
DeactivationAction <Action>;
UninstallAction <Action>;
```

## Example

```
// HelloWorld.rhe

WPPlugin HelloWorld
{
    Action RunThisActionOnActivation '// Put your plugin activation code here ';
    ActivationAction HelloWorld.RunThisActionOnActivation;

    Action CleanUpOnDeactivation '// Put your plugin deactivation code here ';
    DeactivationAction HelloWorld.CleanUpOnDeactivation;

    Action CleanUpOnUninstallation '// Put your plugin uninstall code here ';
    UninstallAction HelloWorld.CleanUpOnUninstallation;
}
```
### Generated code

```php
<?php

// ...

function HelloWorld_CleanUpOnDeactivation() {
    // Put your plugin deactivation code here 
}

function HelloWorld_CleanUpOnUninstallation() {
    // Put your plugin uninstall code here 
}

function HelloWorld_RunThisActionOnActivation() {
    // Put your plugin activation code here 
}

// ,,,

register_activation_hook( __FILE__, function () {
    HelloWorld_RunThisActionOnActivation ();

    /*WPPluginInfo ActivationHook HelloWorld*/
} );

register_deactivation_hook( __FILE__, function () {
    HelloWorld_CleanUpOnDeactivation ();

    /*WPPluginInfo DeactivationHook HelloWorld*/
} );

// NOTE: register_uninstall_hook callback cannot be anonymous function
register_uninstall_hook( __FILE__, 'HelloWorld_uninstall' );

function HelloWorld_uninstall() {
    HelloWorld_CleanUpOnUninstallation ();

    /*WPPluginInfo UninstallHook HelloWorld*/
} );

// ...
```
