using UnityEngine;

namespace Assets.Scripts.Framework.IO.Persistence.Driver
{
	public class PlayerPrefsStorageDriver : IStorageDriver
	{
		public string GetData(string key)
		{
			return PlayerPrefs.GetString(key, null);
		}

		public void SetData(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}
	}
}