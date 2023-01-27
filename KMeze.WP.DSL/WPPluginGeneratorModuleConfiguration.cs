using System.ComponentModel.Composition;
using Autofac;

namespace KMeze.WP.DSL
{
    [Export(typeof(Module))]
    public class WPPluginGeneratorModuleConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var pluginRegistration = builder.GetRhetosPluginRegistration();
            pluginRegistration.FindAndRegisterPlugins<IWPPluginConceptCodeGenerator>();
            base.Load(builder);
        }
    }
}