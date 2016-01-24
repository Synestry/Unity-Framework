using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Framework.IO.Persistence.Driver;
using Assets.Scripts.Framework.IO.Persistence.Migrations;
using Assets.Scripts.Framework.IO.Persistence.Models;

using Newtonsoft.Json;

namespace Assets.Scripts.Framework.IO.Persistence
{
	public class PersistenceManager
	{
		private IStorageDriver StorageDriver { get; set; }

		private List<PersistableObject> PersistableObjects { get; set; }

		private List<AbstractMigration> PersistenceMigrations { get; set; }

		public PersistenceManager()
		{
			//TODO Write storage drivers for iCloud/Google Drive implementation for cross device support
			StorageDriver = new PlayerPrefsStorageDriver();

			PersistableObjects = new List<PersistableObject>();

			PersistenceMigrations = new List<AbstractMigration>();

            AddPersistable(new SettingsData());
		}

		/// <summary>
		///     Adds a persistable to the persistence component.
		///     If the persistable object exists in the storage provider, it will be populated from the existing data.
		///     If the persistable object is not found in the storage provider, the default persistable object will
		///     be added to the storage.
		/// </summary>
		public void AddPersistable<T>(T persistable) where T : PersistableObject
		{
			PersistableObjects.Add(persistable);

			if (PersistableExistsInStorage(persistable))
			{
				PopulatePersitable(persistable, RunMigrations(persistable, StorageDriver.GetData(persistable.Key)));
			}
			else
			{
				StorePersistable(persistable);
			}

			persistable.OnSaveRequest += () => StorePersistable(persistable);
		}


		/// <summary>
		///     Adds a migration to be run when populating a persistable object.
		/// </summary>
		public void AddMigration(AbstractMigration migration)
		{
			PersistenceMigrations.Add(migration);
		}

		public T GetPersitable<T>() where T : PersistableObject
		{
			return PersistableObjects.OfType<T>().SingleOrDefault();
		}

		private bool PersistableExistsInStorage(PersistableObject persistable)
		{
			return !string.IsNullOrEmpty(StorageDriver.GetData(persistable.Key));
		}

		/// <summary>
		///     Populates a persistable object with data from the storage provider.
		///     WARNING: This will overwrite any data already set on the persistable object.
		/// </summary>
		private void PopulatePersitable<T>(T persistable, string data) where T : PersistableObject
		{
			var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
			JsonConvert.PopulateObject(data, persistable, serializerSettings);
		}

		/// <summary>
		///     Recursively runs migrations on the provided data.
		/// </summary>
		/// <returns>
		///		Post migration data string. This will be the same as the input if no migrations were required.
		/// </returns>
		private string RunMigrations(PersistableObject persistable, string data)
		{
			var migrationsForPersistable = PersistenceMigrations.Where(m => m.PersistenceKey == persistable.Key).ToList();

			migrationsForPersistable.ForEach(m =>
			{
				//Recursively run migrations until all are complete.
				if (m.MigrationRequired(data))
				{
					Debug.LogWarning(string.Format("Old persistence data found. Running migration '{0}'", m.GetType().Name));
					RunMigrations(persistable, data = m.Execute());
				}
			});

			return data;
		}

		/// <summary>
		///     Stores a persistable object in the storage provider.
		///     WARNING: This will overwrite any data already set in the storage provider.
		/// </summary>
		private void StorePersistable<T>(T persistable) where T : PersistableObject
		{
			string jsonData = JsonConvert.SerializeObject(persistable);

			StorageDriver.SetData(persistable.Key, jsonData);
		}
	}
}