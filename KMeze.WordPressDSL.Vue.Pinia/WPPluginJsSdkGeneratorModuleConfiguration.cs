using System.ComponentModel.Composition;
using Autofac;

namespace KMeze.WordPressDSL.JsSdk
{
    [Export(typeof(Module))]
    public class WPPluginJsSdkGeneratorModuleConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var pluginRegistration = builder.GetRhetosPluginRegistration();
            pluginRegistration.FindAndRegisterPlugins<IWPPluginJsSdkConceptCodeGenerator>();
            base.Load(builder);
        }
    }
}