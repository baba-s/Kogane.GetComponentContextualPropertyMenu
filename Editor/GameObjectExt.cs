using System.Linq;
using UnityEngine;

namespace UniGetComponentContextualPropertyMenu.Internal
{
	/// <summary>
	/// GameObject 型の拡張メソッドを管理するクラス
	/// </summary>
	internal static class GameObjectExt
	{
		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// string で型名を指定できる GetComponentInChildren
		/// </summary>
		public static Component GetComponentInChildren
		(
			this GameObject self,
			string          type,
			bool            includeInactive = false
		)
		{
			return self
					.GetComponentsInChildren<Transform>( includeInactive )
					.Select( x => x.gameObject.GetComponent( type ) )
					.FirstOrDefault( x => x != null )
				;
		}

		/// <summary>
		/// string で型名を指定できる GetComponentInChildren（自分自身は含まない）
		/// </summary>
		public static Component GetComponentInChildrenOnly
		(
			this GameObject self,
			string          type,
			bool            includeInactive = false
		)
		{
			return self
					.GetComponentsInChildren<Transform>( includeInactive )
					.Select( x => x.gameObject.GetComponent( type ) )
					.Where( x => x != null )
					.FirstOrDefault( x => x.gameObject != self )
				;
		}

		/// <summary>
		/// string で型名を指定できる GetComponentsInChildren
		/// </summary>
		public static Component[] GetComponentsInChildren
		(
			this GameObject self,
			string          type,
			bool            includeInactive = false
		)
		{
			return self
				.GetComponentsInChildren<Transform>( includeInactive )
				.Select( x => x.gameObject.GetComponent( type ) )
				.Where( x => x != null )
				.ToArray();
			;
		}

		/// <summary>
		/// string で型名を指定できる GetComponentsInChildren（自分自身は含まない）
		/// </summary>
		public static Component[] GetComponentsInChildrenOnly
		(
			this GameObject self,
			string          type,
			bool            includeInactive = false
		)
		{
			return self
				.GetComponentsInChildren<Transform>( includeInactive )
				.Select( x => x.gameObject.GetComponent( type ) )
				.Where( x => x != null )
				.Where( x => x.gameObject != self )
				.ToArray();
			;
		}

		/// <summary>
		/// string で型名を指定できる GetComponentInParent
		/// </summary>
		public static Component GetComponentInParent
		(
			this GameObject self,
			string          type,
			bool            includeInactive = false
		)
		{
			return self
					.GetComponentsInParent<Transform>( includeInactive )
					.Select( x => x.gameObject.GetComponent( type ) )
					.FirstOrDefault( x => x != null )
				;
		}

		/// <summary>
		/// string で型名を指定できる GetComponentInParent（自分自身は含まない）
		/// </summary>
		public static Component GetComponentInParentOnly
		(
			this GameObject self,
			string          type,
			bool            includeInactive = false
		)
		{
			return self
					.GetComponentsInParent<Transform>( includeInactive )
					.Select( x => x.gameObject.GetComponent( type ) )
					.Where( x => x != null )
					.FirstOrDefault( x => x.gameObject != self )
				;
		}

		/// <summary>
		/// string で型名を指定できる GetComponentsInParent
		/// </summary>
		public static Component[] GetComponentsInParent
		(
			this GameObject self,
			string          type,
			bool            includeInactive = false
		)
		{
			return self
				.GetComponentsInParent<Transform>( includeInactive )
				.Select( x => x.gameObject.GetComponent( type ) )
				.Where( x => x != null )
				.ToArray();
			;
		}

		/// <summary>
		/// string で型名を指定できる GetComponentsInParent（自分自身は含まない）
		/// </summary>
		public static Component[] GetComponentsInParentOnly
		(
			this GameObject self,
			string          type,
			bool            includeInactive = false
		)
		{
			return self
				.GetComponentsInParent<Transform>( includeInactive )
				.Select( x => x.gameObject.GetComponent( type ) )
				.Where( x => x != null )
				.Where( x => x.gameObject != self )
				.ToArray();
			;
		}
	}
}