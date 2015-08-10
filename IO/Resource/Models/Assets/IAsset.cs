using Assets.Scripts.Framework.Entity;

using UnityEngine;

namespace Assets.Scripts.Framework.IO.Resource.Models.Assets
{
	public interface IAsset : IDestroyable
	{
		string ID { get; set; }

		string Path { get; set; }

		Object Resource { get; set; }

		bool Loaded { get; set; }
	}
}