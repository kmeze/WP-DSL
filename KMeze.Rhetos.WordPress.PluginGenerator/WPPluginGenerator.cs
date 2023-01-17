using Rhetos.Compiler;
using Rhetos.Extensibility;
using Rhetos.Dom;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace KMeze.Rhetos.WordPress.PluginGenerator
{
    [Export(typeof(IGenerator))]
    public class WPPluginGenerator : IGenerator
    {
        private readonly IPluginsContainer<IWPPluginConceptCodeGenerator> _pluginRepository;
        private readonly ICodeGenerator _codeGenerator;
        private readonly ISourceWriter _sourceWriter;

        public WPPluginGenerator(
            IPluginsContainer<IWPPluginConceptCodeGenerator> plugins,
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
                _sourceWriter.Add(sourceFile.Key + ".php", sourceFile.Value);
        }
    }
}