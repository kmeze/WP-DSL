﻿using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(EnumInfo))]
    public class EnumCodeGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (EnumInfo)conceptInfo;

            string snippet = $@"public ?string ${info.Name} = null;
    ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.ClassPropertyTag, info.DataStructure);

            snippet = $@"$dataStructure->{info.Name} = $object->{info.Name};
        ";
            codeBuilder.InsertCode(snippet, DataStructureCodeGenerator.DataClassParsePropertyTag, info.DataStructure);

            if (info.DataStructure is EntityInfo)
            {
                snippet = $@",{info.Name} ENUM ({info.Values})";
                codeBuilder.InsertCode(snippet, PropertyCodeGenerator.DbDeltaPropertyColumnNameTag, info);
            }
        }
    }
}