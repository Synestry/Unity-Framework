namespace Assets.Scripts.Framework.IO.Persistence.Driver
{
	public interface IStorageDriver
	{
		string GetData(string key);

		void SetData(string key, string value);
	}
}