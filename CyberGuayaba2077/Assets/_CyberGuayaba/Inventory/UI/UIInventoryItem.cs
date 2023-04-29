using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;public class UIInventoryItem : MonoBehaviour
{
    [SerializeField] TMP_Text txtName;
    [SerializeField] TMP_Text txtCount;
    [SerializeField] Image imgIcon;

    public void SetItem(InventoryEntry entry)
    {
        txtName.text = entry.itemType.itemName;
        txtCount.SetText("x{0}", entry.quantity);
        imgIcon.sprite = entry.itemType.itemIcon;
    }
}
