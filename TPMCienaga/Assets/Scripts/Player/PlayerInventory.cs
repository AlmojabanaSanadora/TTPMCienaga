using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasHolyWater = false;
    public bool hasCrucifix = false;

    public InventoryUIManager uiManager; // Asignar desde el inspector

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
}
