using Newtonsoft.Json;

namespace Assets.Scripts.Framework.IO.Persistence.Models
{
	public delegate void RequestSaveEvent();

	public class PersistableObject
	{
		[JsonIgnore]
		public string Key { get; private set; }

		public string PersistenceVersion { get { return "1.0.1"; } }

		public PersistableObject(string key)
		{
			Key = key;
		}

		public event RequestSaveEvent OnSaveRequest;

		public void Save()
		{
			OnSaveRequest();
		}
	}
}