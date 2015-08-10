using Assets.Scripts.Framework.IO.Persistence;
using Assets.Scripts.Framework.IO.Persistence.Migrations;
using Assets.Scripts.Framework.IO.Persistence.Models;

using Newtonsoft.Json.Linq;

using NUnit.Framework;

using UnityEngine;

namespace Assets.Scripts.Framework.Tests.Editor
{
	[TestFixture]
	[Category("Persistence Tests")]
	internal class PersistenceTest
	{
		public string RandomKey { get { return "testPersistable" + Random.Range(1, 999999); }}

		[Test]
		public void TestPersistableObject()
		{
			var manager1 = new PersistenceManager();
			var manager2 = new PersistenceManager();
			var manager3 = new PersistenceManager();
			var randomKey = RandomKey;

			//Because this key doesn't exist in the persistence, it should be added automatically.
			//This means TestNumber should be stored into persistence as 5.
			var testPersistable1 = new TestPersistable(randomKey) { TestNumber = 5 };
			manager1.AddPersistable(testPersistable1);

			var testPersistable2 = new TestPersistable(randomKey) { TestNumber = 0 };
			manager2.AddPersistable(testPersistable2);

			//Because this key already exists in the persistence (set by testPersistable1), it should be set back to the stored value.
			Assert.IsTrue(testPersistable2.TestNumber == 5);

			//Set the stored number to 10 and save it.
			testPersistable2.TestNumber = 10;
			testPersistable2.Save();

			var testPersistable3 = new TestPersistable(randomKey) { TestNumber = 5 };
			manager3.AddPersistable(testPersistable3);

			//The object testnumber should be set to 10 as that was the last stored value.
			Assert.IsTrue(testPersistable3.TestNumber == 10);
		}

		[Test]
		public void TestMigration()
		{
			//setup test
			var persistence = new PersistenceManager();
			var randomKey = RandomKey;
			var newPersistenceVersionNumber = "9.9.9"; 

			//Add a new persistable object with an initial value of 5. Add it to the persistence and save it to storage.
			var testPersistable = new TestPersistable(randomKey) { TestNumber = 5 };
			persistence.AddPersistable(testPersistable);
			testPersistable.Save();

			//Create new persistable with same data key but different data structure to represent changes to TestPersistable class.
			var postMigrationPersistable = new PostMigrationTestPersistable(randomKey);

			//Add migration with same key.
			persistence.AddMigration(new TestMigration(testPersistable.PersistenceVersion, newPersistenceVersionNumber, randomKey));

			//Add new persistable to be migrated. This should execute the TestMigration.
			persistence.AddPersistable(postMigrationPersistable);

			//Check the persistence value has been migrated from TestPersistable to PostMigrationTestPersistable
			Assert.AreEqual(postMigrationPersistable.TestNumber2, 5);
		}
	}

	internal class TestMigration : AbstractMigration
	{
		public TestMigration(string preMigrationVersion, string postMigrationVersion, string persistenceKey) : base(preMigrationVersion, postMigrationVersion, persistenceKey) {}

		private const string OldPropertyName = "TestNumber";
		private const string NewPropertyName = "TestNumber2";
		
		protected override void ConvertData()
		{
			//Set the TestNumber2 to the value of TestNumber.
			Data[NewPropertyName] = Data[OldPropertyName];
			//Remove the TestNumber property from the data.
			(Data as JObject).Remove(OldPropertyName);
		}
	}

	internal class TestPersistable : PersistableObject
	{
		public int TestNumber { get; set; }

		public TestPersistable(string key) : base(key) {}
	}

	internal class PostMigrationTestPersistable : PersistableObject
	{
		public int TestNumber2 { get; set; }

		public PostMigrationTestPersistable(string key) : base(key) { }
	}
}
