using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryEntry {
	public ItemType itemType;
	public int quantity;
}

public class InventoryComponent : MonoBehaviour
{

	public List<InventoryEntry> items = new List<InventoryEntry>();

	public UIInventory uiInventory;

	void Start() {
		uiInventory.ShowInventory(this);
	}


	InventoryEntry GetEntry(ItemType inItemType, bool create = false) 
	{
		InventoryEntry entry = null;
		int numCurrentItems = items.Count;
		for (int idx = 0; idx < numCurrentItems; ++idx)
		{
			if (items[idx].itemType == inItemType)
			{
				entry = items[idx];
				break;
			}
		}
		if (entry == null && create) {
			entry = new InventoryEntry();
			entry.itemType = inItemType;
			items.Add(entry);
		}
		return entry;
	}

	void GiveItem(ItemType inItemType, int amount)
	{
		InventoryEntry entry = GetEntry(inItemType, create:true);
		entry.quantity += amount;
	}

	bool HasItemAmount(ItemType inItemType, int amount)
	{
		InventoryEntry entry = GetEntry(inItemType, create:true);
		return entry.quantity >= amount;
	}

	// Returns true if the player has the required amount
	bool TryTakeItem(ItemType inItemType, int amount)
	{
		InventoryEntry entry = GetEntry(inItemType, create:true);
		if (entry.quantity >= amount) {
			entry.quantity -= amount;
			return true;
		}
		return false;
	}
}
