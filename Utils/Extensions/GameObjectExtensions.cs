using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Framework.Utils.Extensions
{
	public static class GameObjectExtensions
	{
		public static GameObject Find(this Enum value, GameObject gameObject = null)
		{
			return GameObject.Find(value.ToString());
		}

        public static List<GameObject> FindGameObjectsWithTag(this GameObject parent, Enum value)
        {
            var objects = new List<GameObject>();

            foreach (Transform transform in parent.GetComponentsInChildren<Transform>())
            {

                if (transform.gameObject.HasTag(value))
                {
                    objects.Add(transform.gameObject);
                }
            }

            return objects;
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