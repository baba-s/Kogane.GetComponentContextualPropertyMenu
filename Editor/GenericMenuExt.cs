using System;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	/// <summary>
	/// GenericMenu 型の拡張メソッドを管理するクラス
	/// </summary>
	internal static class GenericMenuExt
	{
		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// GUIContent を string で指定できる AddItem 関数
		/// </summary>
		public static void AddItem
		(
			this GenericMenu self,
			string           text,
			Action           func
		)
		{
			self.AddItem( new GUIContent( text ), false, () => func() );
		}
	}
}