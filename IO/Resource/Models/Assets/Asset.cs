using Newtonsoft.Json;

using UnityEngine;

namespace Assets.Scripts.Framework.IO.Resource.Models.Assets
{
	public class Asset : IAsset
	{
		public string ID { get; set; }

		public string Path { get; set; }

		[JsonIgnore]
		public Object Resource { get; set; }

		[JsonIgnore]
		public bool Loaded { get; set; }

		public void Destroy()
		{
			Object.Destroy(Resource);
			Loaded = false;
		}
	}
}