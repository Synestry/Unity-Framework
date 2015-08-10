using System;

using UnityEngine;

namespace Assets.Scripts.Framework.Utils.Extensions
{
	public static class GameObjectExtensions
	{
		public static GameObject Find(this Enum value, GameObject gameObject = null)
		{
			return GameObject.Find(value.ToString());
		}

		public static bool HasTag(this GameObject gameObject, Enum tag)
		{
			return gameObject.CompareTag(tag.ToString());
		}

		public static T GetComponent<T>(this Enum value) where T : UnityEngine.Component
		{
			GameObject obj = Find(value);
			return obj != null ? obj.GetComponent<T>() : null;
		}

		public static T GetComponentInChildren<T>(this Enum value) where T : UnityEngine.Component
		{
			GameObject obj = Find(value);
			return obj != null ? obj.GetComponentInChildren<T>() : null;
		}
	}
}