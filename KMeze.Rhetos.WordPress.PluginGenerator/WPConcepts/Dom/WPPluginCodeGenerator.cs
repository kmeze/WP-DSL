using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(WPPluginInfo))]
    public class WPPluginCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<WPPluginInfo> BodyTag = "PluginBody";
        public static readonly CsTag<WPPluginInfo> ActivationTag = "ActivationHook";
        public static readonly CsTag<WPPluginInfo> DeactivationTag = "DeactivationHook";
        public static readonly CsTag<WPPluginInfo> UninstallTag = "UninstallHook";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (WPPluginInfo)conceptInfo;

            codeBuilder.InsertCodeToFile(
$@"<?php
/**
 * Plugin Name: {info.Name}
 */

// Exit if accessed directly
defined( 'ABSPATH' ) || exit;

{BodyTag.Evaluate(info)}

register_activation_hook( __FILE__, function () {{
    require_once( ABSPATH . 'wp-admin/includes/upgrade.php' );
    global $wpdb;

    {ActivationTag.Evaluate(info)}
}} );

register_deactivation_hook( __FILE__, function () {{
    global $wpdb;

    {DeactivationTag.Evaluate(info)}
}} );

// NOTE: register_uninstall_hook callback cannot be anonymus function
register_uninstall_hook( __FILE__, '{info.Name}_uninstall' );

function {info.Name}_uninstall () {{
    {UninstallTag.Evaluate(info)}
}}", $"{Path.Combine( Path.Combine("WordPress", info.Name), info.Name)}");
        }
    }
}