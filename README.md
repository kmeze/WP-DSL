# 

WP-DSL is the implementation of WordPress [domain-specific language](https://en.wikipedia.org/wiki/Domain-specific_language) (DSL). It simplifies WordPress plugin development in the way that developers (mostly) use a specific high-level programming language to create WordPress plugins instead of writing extensive PHP code.

WP-DSL is a plugin package
built on the [Rhetos platform](https://github.com/Rhetos/Rhetos). See [rhetos.org](http://www.rhetos.org/)
and [Rhetos wiki](https://github.com/Rhetos/Rhetos/wiki) for more information on Rhetos. 
See [WordPress Plugin Handbook](https://developer.wordpress.org/plugins/) for more information on WordPress plugin
development.

## Features

* generates WordPress main plugin file with
  required [header comment](https://developer.wordpress.org/plugins/plugin-basics/header-requirements/) and [activation / deactivation / uninstall Hooks](https://developer.wordpress.org/plugins/plugin-basics/activation-deactivation-hooks/)
* simplifies working with custom Non-Post Type entities:
    * generates PHP classes for custom entities
    * generates [dbDelta function](https://developer.wordpress.org/reference/functions/dbdelta/) calls for create and
      update tables in the database
    * generates [REST API](https://developer.wordpress.org/rest-api/) endpoints and controller classes
    * generates database clean-up code

## List of WP-DSL concepts

### Basic concepts

* [WPPlugin](Docs/WPPlugin.md) - generates WordPress plugin file
* [ActivationAction / DeactivationAction / UninstallAction](Docs/ActivationAction-DeactivationAction-UninstallAction.md) - execute the [Action](Docs/Action.md) in the plugin activation / deactivation / uninstall hook

### Low-level concepts

In a low-level concept, you can do anything you need because you a writing common WordPress PHP code that will be inserted in a plugin file. It is recommended to use low-level concepts as less you can, only as a workaround if there isn't some higher-level concept for your need.

* [Action](Docs/Action.md) - create (PHP) function in plugin file
* [Code](Docs/Code.md) - inserts PHP code into plugin file

## Usage

1. Create local folder (e.g. MyPlugin)
2. Navigate into the new folder and run the following commands in terminal (Mac/Linux) or command prompt 'Windows':
    ```
    dotnet new classlib
    dotnet add package KMeze.WP.DSL --prerelease
    dotnet add package Rhetos.MSBuild
    dotnet add package Microsoft.Extensions.Configuration
    ```

3. Create new file with `.rhe` extension (e.g. MyPlugin.rhe) and paste following code:
    ```
    WPPlugin MyPlugin;
    ```
4. Build project by running:
    ```
    dotnet build
    ```

Your new plugin is created under obj/Rhetos/Source/WordPress in its own subfolder. Copy/paste or link plugin folder to WordPress plugins folder (wordpress/wp-content/plugins) and activate it like any other WordPress plugin.
