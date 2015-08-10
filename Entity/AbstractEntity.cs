using UnityEngine;

namespace Assets.Scripts.Framework.Entity
{
	public abstract class AbstractEntity : IDestroyable
	{
		public GameObject GameObject { get; private set; }

		public virtual void Destroy()
		{
			Object.Destroy(GameObject);
			GameObject = null;
		}

		protected void Initialise(Object resource, GameObject parent)
		{
			Initialise(resource);
			GameObject.transform.SetParent(parent.transform, false);
		}

		protected virtual void Initialise(Object resource)
		{
			GameObject = (GameObject)Object.Instantiate(resource);
		}

		public virtual void Update(float dt) {}
	}
}