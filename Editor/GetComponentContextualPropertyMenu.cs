using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    /// <summary>
    /// 参照型のパラメータを右クリックした時のメニューに GetComponent を追加するエディタ拡張
    /// </summary>
    [InitializeOnLoad]
    internal static class GetComponentContextualPropertyMenu
    {
        //================================================================================
        // 関数(static)
        //================================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static GetComponentContextualPropertyMenu()
        {
            EditorApplication.contextualPropertyMenu -= OnMenu;
            EditorApplication.contextualPropertyMenu += OnMenu;
        }

        /// <summary>
        /// GetComponent をメニューに追加します
        /// </summary>
        private static void OnMenu( GenericMenu menu, SerializedProperty property )
        {
            var obj    = property.serializedObject;
            var target = obj.targetObject as Component;

            if ( target == null || target.gameObject == null ) return;

            var copiedProperty = property.Copy();
            var isArray        = copiedProperty.isArray;
            var propertyType   = copiedProperty.propertyType;

            // 配列の場合
            if ( isArray && propertyType == SerializedPropertyType.Generic && Regex.IsMatch( property.arrayElementType, @"PPtr<\$(.*?)>" ) )
            {
                menu.AddItem( "GetComponentsInParent", () => OnGetComponents( copiedProperty, target, ( gameObject,       typeName ) => gameObject.GetComponentsInParent( typeName, true ) ) );
                menu.AddItem( "GetComponentsInParentOnly", () => OnGetComponents( copiedProperty, target, ( gameObject,   typeName ) => gameObject.GetComponentsInParentOnly( typeName, true ) ) );
                menu.AddItem( "GetComponentsInChildren", () => OnGetComponents( copiedProperty, target, ( gameObject,     typeName ) => gameObject.GetComponentsInChildren( typeName, true ) ) );
                menu.AddItem( "GetComponentsInChildrenOnly", () => OnGetComponents( copiedProperty, target, ( gameObject, typeName ) => gameObject.GetComponentsInChildrenOnly( typeName, true ) ) );
            }
            // 配列ではない参照型のパラメータの場合
            else if ( !isArray && propertyType == SerializedPropertyType.ObjectReference )
            {
                menu.AddItem( "GetComponent", () => OnGetComponent( copiedProperty, target, ( gameObject, typeName ) => gameObject.GetComponent( typeName ) ) );
                menu.AddSeparator( string.Empty );
                menu.AddItem( "GetComponentInParent", () => OnGetComponent( copiedProperty, target, ( gameObject,       typeName ) => gameObject.GetComponentInParent( typeName, true ) ) );
                menu.AddItem( "GetComponentInParentOnly", () => OnGetComponent( copiedProperty, target, ( gameObject,   typeName ) => gameObject.GetComponentInParentOnly( typeName, true ) ) );
                menu.AddItem( "GetComponentInChildren", () => OnGetComponent( copiedProperty, target, ( gameObject,     typeName ) => gameObject.GetComponentInChildren( typeName, true ) ) );
                menu.AddItem( "GetComponentInChildrenOnly", () => OnGetComponent( copiedProperty, target, ( gameObject, typeName ) => gameObject.GetComponentInChildrenOnly( typeName, true ) ) );
            }
        }

        /// <summary>
        /// GetComponents を実行するメニューを作成します
        /// </summary>
        private static void OnGetComponents
        (
            SerializedProperty                    property,
            Component                             target,
            Func<GameObject, string, Component[]> getComponent
        )
        {
            var typeName = GetPropertyType( property.arrayElementType );
            var list     = getComponent( target.gameObject, typeName );

            property.serializedObject.Update();
            property.ClearArray();

            for ( var i = 0; i < list.Length; i++ )
            {
                property.InsertArrayElementAtIndex( i );
                var element = property.GetArrayElementAtIndex( i );
                element.objectReferenceValue = list[ i ];
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// GetComponent を実行するメニューを作成します
        /// </summary>
        private static void OnGetComponent
        (
            SerializedProperty                  property,
            Component                           target,
            Func<GameObject, string, Component> getComponent
        )
        {
            var typeName = GetPropertyType( property.type );
            var com      = getComponent( target.gameObject, typeName );

            property.serializedObject.Update();
            property.objectReferenceValue = com;
            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// パラメータの型名を返します
        /// </summary>
        private static string GetPropertyType( string type )
        {
            var match = Regex.Match( type, @"PPtr<\$(.*?)>" );
            return match.Success ? match.Groups[ 1 ].Value : type;
        }
    }
}