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

    // Interno: mantiene el control de slots actuales
    private Dictionary<string, GameObject> itemSlots = new Dictionary<string, GameObject>();

    public void AddItemToUI(string itemName)
    {
        if (itemSlots.ContainsKey(itemName))
        {
            // Ya existe → solo aumentar el número
            GameObject slot = itemSlots[itemName];
            Text numberText = slot.transform.Find("Number").GetComponent<Text>();
            int currentCount = int.Parse(numberText.text);
            numberText.text = (currentCount + 1).ToString();
        }
        else
        {
            // Crear nuevo slot
            InventoryItemIcon iconData = itemIcons.Find(i => i.itemName == itemName);
            if (iconData != null)
            {
                GameObject newSlot = Instantiate(slotPrefab, contentPanel);
                Image iconImage = newSlot.transform.Find("Icon").GetComponent<Image>();
                Text numberText = newSlot.transform.Find("Number").GetComponent<Text>();

                iconImage.sprite = iconData.icon;
                numberText.text = "1";

                itemSlots[itemName] = newSlot;
            }
        }
    }

    public void ClearInventoryUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        itemSlots.Clear();
    }
}
