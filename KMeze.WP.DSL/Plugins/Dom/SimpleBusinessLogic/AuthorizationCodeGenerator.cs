using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(AuthorizationInfo))]
    public class AuthorizationCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (AuthorizationInfo)conceptInfo;

            string snippet = $@"if ( (bool) {info.DataStructure.Plugin.Slug}_{info.Callback.Name} () ) return true;

        ";
            codeBuilder.InsertCode(snippet, RepositoryDataStructureCodeGenerator.RestControllerAuthorizationTag, info.DataStructure);
        }
    }
}