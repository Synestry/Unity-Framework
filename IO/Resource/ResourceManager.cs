using System;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Framework.Event;
using Assets.Scripts.Framework.IO.Resource.Models;
using Assets.Scripts.Framework.IO.Resource.Models.Assets;

using Newtonsoft.Json;

using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Framework.IO.Resource
{
	public class ResourceManager : IEventDispatcher
	{
		public List<IResourceDescriptor> Descriptors { get; private set; }

		protected GameObject AssetPool { get; set; }

		protected ResourceManager()
		{
			Descriptors = new List<IResourceDescriptor>();

			AssetPool = new GameObject("Asset Pool");
			AssetPool.SetActive(false);

			EventDispatcher = new EventDispatcher(GameManager.Instance.EventDispatcher);
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

		protected T LoadDescriptor<T>(string descriptorPath) where T : IResourceDescriptor
		{
			var asset = Resources.Load<TextAsset>(descriptorPath);

			if (asset == null)
			{
				throw new Exception(string.Format("Could not find descriptor file: {0}", descriptorPath));
			}

			return JsonConvert.DeserializeObject<T>(asset.text);
		}
	}
}