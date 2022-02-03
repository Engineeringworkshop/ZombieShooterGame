using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
	[SerializeField] protected ItemSlot[] itemSlots;

	
	public virtual bool CanAddItem(Item item, int amount = 1)
	{
		int freeSpaces = 0;

		foreach (ItemSlot itemSlot in ItemSlots)
		{
			if (itemSlot.Item == null || itemSlot.Item.ID == item.ID)
			{
				freeSpaces += item.MaximumStacks - itemSlot.Amount;
			}
		}
		return freeSpaces >= amount;
	}

	public virtual bool AddItem(Item item)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].CanAddStack(item))
			{
				ItemSlots[i].Item = item;
				ItemSlots[i].Amount++;
				return true;
			}
		}

		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item == null)
			{
				ItemSlots[i].Item = item;
				ItemSlots[i].Amount++;
				return true;
			}
		}
		return false;
	}

	public virtual bool RemoveItem(Item item)
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			if (itemSlots[i].Item == item)
			{
				itemSlots[i].Amount--;
				return true;
			}
		}
		return false;
	}

	public virtual Item RemoveItem(string itemID)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			Item item = ItemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				ItemSlots[i].Amount--;
				return item;
			}
		}
		return null;
	}

	public virtual int ItemCount(string itemID)
	{
		int number = 0;

		for (int i = 0; i < ItemSlots.Count; i++)
		{
			Item item = ItemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				number += ItemSlots[i].Amount;
			}
		}
		return number;
	}

	public void Clear()
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item != null && Application.isPlaying)
			{
				ItemSlots[i].Item.Destroy();
			}
			ItemSlots[i].Item = null;
			ItemSlots[i].Amount = 0;
		}
	}
}
