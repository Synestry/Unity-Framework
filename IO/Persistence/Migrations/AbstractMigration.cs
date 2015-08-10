using Assets.Scripts.Framework.Utils.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assets.Scripts.Framework.IO.Persistence.Migrations
{
	public abstract class AbstractMigration
	{
		public string PreMigrationVersion { get; private set; }
		public string PostMigrationVersion { get; private set; }

		public string PersistenceKey { get; private set; }

		protected JToken Data { get; private set; }

		private const string VersionMemberName = "PersistenceVersion";

		protected AbstractMigration(string preMigrationVersion, string postMigrationVersion, string persistenceKey)
		{
			PreMigrationVersion = preMigrationVersion;
			PostMigrationVersion = postMigrationVersion;
			PersistenceKey = persistenceKey;
		}

		public bool MigrationRequired(string oldData)
		{
			if(Data == null) Data = JsonConvert.DeserializeObject<JToken>(oldData);
			var version = Data[VersionMemberName].ToString();
			return version.CompareVersionsString(PreMigrationVersion) == 0;
		}

		public string Execute()
		{
			ConvertData();
			SetVersion();
			return JsonConvert.SerializeObject(Data);
		}

		private void SetVersion()
		{
			Data[VersionMemberName] = PostMigrationVersion;
		}

		protected abstract void ConvertData();
	}
}
