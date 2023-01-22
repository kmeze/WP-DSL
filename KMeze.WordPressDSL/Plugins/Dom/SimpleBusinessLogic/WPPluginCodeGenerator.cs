using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(WPPluginInfo))]
    public class WPPluginCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<WPPluginInfo> BodyTag = "PluginBody";
        public static readonly CsTag<WPPluginInfo> ActivationHookTag = "ActivationHook";
        public static readonly CsTag<WPPluginInfo> DeactivationHookTag = "DeactivationHook";
        public static readonly CsTag<WPPluginInfo> UninstallHookTag = "UninstallHook";

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
    {ActivationHookTag.Evaluate(info)}
}} );

register_deactivation_hook( __FILE__, function () {{
    {DeactivationHookTag.Evaluate(info)}
}} );

// NOTE: register_uninstall_hook callback cannot be anonymous function
register_uninstall_hook( __FILE__, '{info.Name}_uninstall' );

function {info.Name}_uninstall() {{
    {UninstallHookTag.Evaluate(info)}
}}", $"{Path.Combine(Path.Combine("WordPress", info.Name), info.Name)}");
        }
    }
}