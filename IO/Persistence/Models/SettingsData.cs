namespace Assets.Scripts.Framework.IO.Persistence.Models
{
	public class SettingsData : PersistableObject
	{
		public bool IsMuted { get; set; }

		public SettingsData() : base("settingsData")
		{
			IsMuted = false;
		}
	}
}