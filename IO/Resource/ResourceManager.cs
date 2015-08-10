using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Framework.Event;
using Assets.Scripts.Framework.IO.Resource.Models;
using Assets.Scripts.Framework.IO.Resource.Models.Assets;

using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Framework.IO.Resource
{
	public class ResourceManager : IEventDispatcher
	{
		public List<IResourceDescriptor> Descriptors { get; private set; }

		protected GameObject AssetPool { get; set; }

		public ResourceManager()
		{
			Descriptors = new List<IResourceDescriptor>();

			AssetPool = new GameObject("Asset Pool");
			AssetPool.SetActive(false);

			EventDispatcher = new EventDispatcher(AbstractGameManager.Instance.EventDispatcher);
		}

		public EventDispatcher EventDispatcher { get; private set; }

		public T GetDescriptor<T>()
		{
			return Descriptors.OfType<T>().SingleOrDefault();
		}

		protected void LoadAsset(IAsset asset, UnityAction callback = null, string basePath = null)
		{
			LoadAsset(new List<IAsset> { asset }, callback, basePath);
		}

		protected void LoadAsset(IEnumerable<IAsset> assets, UnityAction callback = null, string basePath = null)
		{
			var loader = new GameObject("Loader").AddComponent<AssetLoader>();
			if (callback != null)
			{
				loader.OnLoadComplete += progress => callback();
			}
			loader.Load(assets, AssetPool, basePath);
		}
	}
}