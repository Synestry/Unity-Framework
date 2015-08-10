using System.Collections.Generic;

namespace Assets.Scripts.Framework.IO.Persistence.Models
{
	public class WalletData : PersistableObject
	{
		public Dictionary<string, int> WalletEntries { get; set; }

		public WalletData() : base("walletData")
		{
			WalletEntries = new Dictionary<string, int>();
		}

		public int GetWalletAmount(string walletId)
		{
			//Set an initial amount of 0 for the entry if it doesn't exist
			if (!WalletEntries.ContainsKey(walletId))
			{
				UpdatePersistedWalletAmount(walletId, 0);
			}
			return WalletEntries[walletId];
		}

		public void UpdatePersistedWalletAmount(string walletId, int amount)
		{
			WalletEntries[walletId] = amount;
			Save();
		}
	}
}