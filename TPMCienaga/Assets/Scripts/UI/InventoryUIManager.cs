using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItemIcon
    {
        public string itemName;
        public Sprite icon;
    }

    public GameObject slotPrefab;        // Prefab del slot
    public Transform contentPanel;       // Panel donde se colocan los ítems
    public List<InventoryItemIcon> itemIcons = new List<InventoryItemIcon>();

    private int itemCount = 0;

    public void AddItemToUI(string itemName)
    {
        InventoryItemIcon iconData = itemIcons.Find(i => i.itemName == itemName);
        if (iconData != null)
        {
            GameObject newSlot = Instantiate(slotPrefab, contentPanel);
            Image iconImage = newSlot.transform.Find("Icon").GetComponent<Image>();
            Text numberText = newSlot.transform.Find("Number").GetComponent<Text>();

            iconImage.sprite = iconData.icon;
            itemCount++;
            numberText.text = itemCount.ToString();
        }
    }

    public void ClearInventoryUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        itemCount = 0;
    }
}
