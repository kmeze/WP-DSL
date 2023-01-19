using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(DropOnDeactivateInfo))]
    public class DropOnDeactivateCedeGenerator : IWPPluginConceptCodeGenerator
    {
            public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
            {
                var info = (DropOnDeactivateInfo)conceptInfo;

                string snippet = $@"$table_name = $wpdb->prefix . '{info.Entity.WPPlugin.Name}_{info.Entity.Name}';
    $wpdb->query( ""DROP TABLE IF EXISTS {{$table_name}}"" );

    ";
                codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.DeactivationHookTag, info.Entity.WPPlugin);
            }
    }
}