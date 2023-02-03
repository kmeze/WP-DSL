using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(WPPluginInfo))]
    public class WPPluginCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public static readonly CsTag<WPPluginInfo> ActionHooksTag = "ActionHooks";
        public static readonly CsTag<WPPluginInfo> FilterHooksTag = "FilterHooks";
        public static readonly CsTag<WPPluginInfo> CallbacksTag = "Callbacks";
        public static readonly CsTag<WPPluginInfo> DataStructureClassesTag = "DataStructureClasses";
        public static readonly CsTag<WPPluginInfo> CodeTag = "CodeTag";
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

/**
 * Action Hooks
 */
{ActionHooksTag.Evaluate(info)}

/**
 * Filter Hooks
 */
{FilterHooksTag.Evaluate(info)}

/**
 * Callbacks
 */
{CallbacksTag.Evaluate(info)}
function {info.Name}_uninstall() {{
    {UninstallHookTag.Evaluate(info)}
}}

/**
 * DataStructure classes
 */
{DataStructureClassesTag.Evaluate(info)}

/**
 * Code concept
 */
{CodeTag.Evaluate(info)}

register_activation_hook( __FILE__, function () {{
    {ActivationHookTag.Evaluate(info)}
}} );
register_deactivation_hook( __FILE__, function () {{
    {DeactivationHookTag.Evaluate(info)}
}} );
register_uninstall_hook( __FILE__, '{info.Name}_uninstall' ); // NOTE: register_uninstall_hook callback cannot be anonymous function"
        , $"{Path.Combine(Path.Combine("WordPress", info.Name), info.Name)}");
        }
    }
}