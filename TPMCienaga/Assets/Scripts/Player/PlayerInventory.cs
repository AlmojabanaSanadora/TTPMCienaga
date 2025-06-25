using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasHolyWater = false;
    public bool hasCrucifix = false;

    public InventoryUIManager uiManager;
    public GameObject inventoryPanel;

    private bool isInventoryOpen = false;

    public bool CanPickupHolyWater() => !hasHolyWater;
    public bool CanPickupCrucifix() => !hasCrucifix;

    public void PickupHolyWater()
    {
        if (!hasHolyWater)
        {
            hasHolyWater = true;
            uiManager.AddItemToUI("HolyWater");
        }
    }

    public void PickupCrucifix()
    {
        if (!hasCrucifix)
        {
            hasCrucifix = true;
            uiManager.AddItemToUI("Crucifix");
        }
    }

    public void UseItems()
    {
        hasHolyWater = false;
        hasCrucifix = false;
        uiManager.ClearInventoryUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (inventoryPanel != null)
            inventoryPanel.SetActive(isInventoryOpen);

        Time.timeScale = isInventoryOpen ? 0f : 1f;
        Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isInventoryOpen;
    }

    public bool IsInventoryOpen() => isInventoryOpen;
}
