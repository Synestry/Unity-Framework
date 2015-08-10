using System;
using System.Collections.Generic;

using Assets.Scripts.Framework.Component.Commerce;
using Assets.Scripts.Framework.Component.Commerce.InventoryCommponent;
using Assets.Scripts.Framework.Component.Commerce.ShopComponent;
using Assets.Scripts.Framework.Component.Commerce.WalletComponent;

using UnityEngine;

namespace Assets.Scripts.Framework.IO.Persistence.Models
{
	public class InventoryData : PersistableObject
	{
		public Dictionary<string, InventoryItem> Items { get; set; }

		private CommerceComponent<Wallet, Shop, Inventory> CommerceComponent
		{
			get { return GameManager.Instance.Get<CommerceComponent<Wallet, Shop, Inventory>>(); }
		}

		public InventoryData() : base("inventoryData")
		{
			Items = new Dictionary<string, InventoryItem>();
		}

		public void AddItem(string itemId, int amount)
		{
			if (amount < 1)
			{
				throw new Exception("Can not add a new item with a quantity less than 1");
			}
			UpdateItem(itemId, amount);
		}

		public void RemoveItem(string itemId, int amount)
		{
			if (amount < 1)
			{
				throw new Exception("Can not remove an item with a quantity less than 1");
			}
			UpdateItem(itemId, amount);
		}

		public int GetOwnedAmount(string itemId)
		{
			if (!Items.ContainsKey(itemId))
			{
				return 0;
			}
			return Items[itemId].Amount;
		}

		private void UpdateItem(string itemId, int amount)
		{
			if (!Items.ContainsKey(itemId))
			{
				CreateItem(itemId, amount);
			}
			else
			{
				Items[itemId].UpdateAmount(amount);
			}

			Debug.Log("Successfully updated " + itemId + " to an amount of " + Items[itemId].Amount);
			Save();
		}

		private void CreateItem(string itemId, int amount)
		{
			Items[itemId] = new InventoryItem(amount);
		}
	}
}