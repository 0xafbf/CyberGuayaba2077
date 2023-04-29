using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
	[SerializeField] Transform container;
	[SerializeField] UIInventoryItem uiItemTemplate;
	List<UIInventoryItem> uiItems = new List<UIInventoryItem>();

	public void ShowInventory(InventoryComponent inventory)
	{
		var items = inventory.items;
		int numItems = inventory.items.Count;
		// add new items if this time the inventory is bigger
		for (int idx = uiItems.Count; idx < numItems; ++idx) {
			UIInventoryItem item = Instantiate(uiItemTemplate, container);
			uiItems.Add(item);
		}

		// update all
		for (int idx = 0; idx < numItems; ++idx)
		{
			uiItems[idx].SetItem(items[idx]);
		}

		// hide remaining items if inventory is smaller
		for (int idx = numItems; idx < uiItems.Count; ++idx) {

		}
	}
}
