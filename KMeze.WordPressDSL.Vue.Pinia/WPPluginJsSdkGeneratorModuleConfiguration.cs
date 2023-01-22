using System.ComponentModel.Composition;
using Autofac;

namespace KMeze.WordPressDSL.Vue.Pinia
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