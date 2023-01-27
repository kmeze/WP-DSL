using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WordPressDSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(AuthorizationInfo))]
    public class AuthorizationCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (AuthorizationInfo)conceptInfo;

            string snippet = $@"if ( (bool) {info.Entity.WPPlugin.Name}_{info.Action.Name} () ) return true;

        ";
            codeBuilder.InsertCode(snippet, EntityCodeGenerator.AuthorizationTag, info.Entity);
        }
    }
}