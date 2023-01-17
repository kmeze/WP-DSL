using System.ComponentModel.Composition;
using Autofac;

namespace KMeze.Rhetos.WordPress.PluginGenerator
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
