using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Framework.IO.Resource.Models.Assets;

using UnityEngine;

namespace Assets.Scripts.Framework.IO.Resource
{
	public class AssetLoader : MonoBehaviour
	{
		public delegate void ProgressEvent(float progress);

		public float CurrentProgress { get; set; }

		private string PathPrefix { get; set; }

		private GameObject AssetPool { get; set; }

		private Queue<IAsset> Assets { get; set; }

		private Dictionary<IAsset, ResourceRequest> Requests { get; set; }

		private bool AllAssetsLoaded { get { return Requests.All(resourceRequest => resourceRequest.Key.Loaded); } }

		public event ProgressEvent OnProgressChanged;

		public event ProgressEvent OnLoadComplete;

		public void Load(IEnumerable<IAsset> assets, GameObject assetPool, string pathPrefix = null)
		{
			Assets = new Queue<IAsset>(assets);
			AssetPool = assetPool;
			PathPrefix = pathPrefix;

			Requests = new Dictionary<IAsset, ResourceRequest>();
			CurrentProgress = 0;
			StartCoroutine(LoadAssets());
		}

		//Starts the asset loading, executes the callback and destroy's itself when complete.
		private IEnumerator LoadAssets()
		{
			while (Assets.Count > 0)
			{
				StartCoroutine(LoadAsset(Assets.Dequeue()));
			}

			//Ensure yield returns atleast once so the OnLoadComplete event is not dispatched before the loader is returned in ResourceManager.
			do
			{
				yield return null;
			}
			while (!AllAssetsLoaded);

			if (OnLoadComplete != null)
			{
				OnLoadComplete(CurrentProgress = 1);
			}

			Destroy(gameObject);
		}

		private IEnumerator LoadAsset(IAsset asset)
		{
			if (!asset.Loaded)
			{
				ResourceRequest request = Resources.LoadAsync(PathPrefix != null ? PathPrefix + "/" + asset.Path : asset.Path);

				Requests[asset] = request;

				yield return request;

				if (request.asset != null)
				{
					asset.Resource = Instantiate(request.asset);

					if (asset.Resource is GameObject)
					{
						((GameObject)(asset.Resource)).transform.SetParent(AssetPool.transform, false);
					}

					asset.Loaded = true;
				}
				else
				{
					Debug.LogError("Failed to load asset! Could not find: " + asset.Path);
				}
			}
		}

		private void Update()
		{
			if (Requests != null)
			{
				CurrentProgress = Requests.Sum(rr => rr.Value.progress) / Requests.Count;
				if (OnProgressChanged != null)
				{
					OnProgressChanged(CurrentProgress);
				}
			}
		}
	}
}