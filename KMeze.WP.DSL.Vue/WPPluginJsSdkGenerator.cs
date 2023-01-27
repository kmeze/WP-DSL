using Rhetos.Compiler;
using Rhetos.Extensibility;
using Rhetos.Dom;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace KMeze.WP.DSL.Vue.Pinia
{
    [Export(typeof(IGenerator))]
    public class WPPluginJsSdkGenerator : IGenerator
    {
        private readonly IPluginsContainer<IWPPluginJsSdkConceptCodeGenerator> _pluginRepository;
        private readonly ICodeGenerator _codeGenerator;
        private readonly ISourceWriter _sourceWriter;

        public WPPluginJsSdkGenerator(
            IPluginsContainer<IWPPluginJsSdkConceptCodeGenerator> plugins,
            ICodeGenerator codeGenerator,
            ISourceWriter sourceWriter)
        {
            _pluginRepository = plugins;
            _codeGenerator = codeGenerator;
            _sourceWriter = sourceWriter;
        }

        public IEnumerable<string> Dependencies => Array.Empty<string>();

        public void Generate()
        {
            var sourceFiles = _codeGenerator.ExecutePluginsToFiles(_pluginRepository, "/*", "*/", null);

            foreach (var sourceFile in sourceFiles)
                _sourceWriter.Add(sourceFile.Key + ".js", sourceFile.Value);
        }
    }
}