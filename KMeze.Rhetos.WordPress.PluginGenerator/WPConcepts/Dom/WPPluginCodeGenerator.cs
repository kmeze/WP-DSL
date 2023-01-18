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
        public static readonly CsTag<WPPluginInfo> BodyTag = "Body";
        public static readonly CsTag<WPPluginInfo> RepositoryMethodTag = "RepositoryMethod";

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

class {info.Name}_Repository {{
    {RepositoryMethodTag.Evaluate(info)}
}}
", $"{Path.Combine( Path.Combine("WordPress", info.Name), info.Name)}");
        }
    }
}