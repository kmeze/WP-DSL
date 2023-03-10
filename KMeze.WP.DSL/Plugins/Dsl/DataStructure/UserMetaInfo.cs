using System.ComponentModel.Composition;
using Rhetos.Dsl;

namespace KMeze.WP.DSL
{
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("UserMeta")]
    public class UserMetaInfo : DataStructureInfo
    {
    }

    [Export(typeof(IConceptMacro))]
    public class UserMetaMacro : IConceptMacro<UserMetaInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(UserMetaInfo conceptInfo, IDslModel existingConcepts)
        {
            var callback = new CallbackInfo
            {
                Plugin = conceptInfo.Plugin,
                Name = $@"{conceptInfo.Name}_register_rest_field",
                Script = $@"register_rest_field( 'user', '{conceptInfo.Plugin.Slug}_{conceptInfo.Name}', array(
            'get_callback' => function ( $user, $key ) {{
                $meta = get_user_meta( $user['id'], $key, true );

                if ( empty ( $meta ) ) return new {conceptInfo.Plugin.Slug}_{conceptInfo.Name}();

                return {conceptInfo.Plugin.Slug}_{conceptInfo.Name}::parse( (object) $meta );;
            }},
            'update_callback' => function ( $value, $user, $key ) {{
                return update_user_meta( $user->id, $key, $value );
            }},
        )
    );"
            };

            var actionHook = new ActionHookWithDefaultPriorityInfo
            {
                Plugin = conceptInfo.Plugin,
                Hook = "rest_api_init",
                Callback = callback,
            };

            return new IConceptInfo[] { callback, actionHook };
        }
    }
}