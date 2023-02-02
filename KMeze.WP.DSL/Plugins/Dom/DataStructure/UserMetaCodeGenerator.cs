using System;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dom.DefaultConcepts;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace KMeze.WP.DSL
{
    [Export(typeof(IWPPluginConceptCodeGenerator))]
    [ExportMetadata(MefProvider.Implements, typeof(UserMetaInfo))]
    public class UserMetaGenerator : IWPPluginConceptCodeGenerator
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (UserMetaInfo)conceptInfo;

            string snippet = $@"add_action( 'rest_api_init', function () {{
    register_rest_field( 'user', '{info.WPPlugin.Name}_{info.Name}', array(
            'get_callback' => function ( $user, $key ) {{
                $meta = get_user_meta( $user['id'], $key, true );

                if ( empty ( $meta ) ) return new {info.WPPlugin.Name}_{info.Name}();

                return {info.WPPlugin.Name}_{info.Name}::parse( (object) $meta );;
            }},
            'update_callback' => function ( $value, $user, $key ) {{
                return update_user_meta( $user->id, $key, $value );
            }},
        )
    );
}} );
";
            codeBuilder.InsertCode(snippet, WPPluginCodeGenerator.ActionHooksTag, info.WPPlugin);
        }
    }
}