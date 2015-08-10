using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Framework.Utils.Extensions
{
	public static class TransformExtensions
	{
		public static Transform SearchChildren(this Transform target, string name)
		{
			if (target == null)
			{
				return null;
			}
			if (target.name == name)
			{
				return target;
			}

			for (int i = 0; i < target.childCount; ++i)
			{
				Transform result = SearchChildren(target.GetChild(i), name);
				if (result != null)
				{
					return result;
				}
			}
			return null;
		}

		public static void DestroyChildren(this Transform target)
		{
			foreach (Transform child in target)
			{
				Object.Destroy(child.gameObject);
			}
		}

		public static void SearchChildren<T>(this Transform target, List<T> components) where T : UnityEngine.Component
		{
			if (target == null)
			{
				return;
			}

			for (int i = 0; i < target.childCount; ++i)
			{
				Transform child = target.GetChild(i);
				var component = child.GetComponent<T>();

				// If this child has the component, add it
				if (component != null)
				{
					components.Add(component);
				}

				// Then search through his children
				SearchChildren(child, components);
			}
		}

		public static T SearchChildren<T>(this Transform target) where T : UnityEngine.Component
		{
			if (target == null)
			{
				return null;
			}

			T component = null;

			for (int i = 0; i < target.childCount; ++i)
			{
				Transform child = target.GetChild(i);
				component = child.GetComponent<T>();

				if (component)
				{
					break;
				}

				component = SearchChildren<T>(child);

				if (component)
				{
					break;
				}
			}

			return component;
		}
	}
}