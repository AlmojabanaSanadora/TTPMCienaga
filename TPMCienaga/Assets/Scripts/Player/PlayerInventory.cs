using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasHolyWater = false;
    public bool hasCrucifix = false;

    public bool CanPickupHolyWater() => !hasHolyWater;
    public bool CanPickupCrucifix() => !hasCrucifix;

    public void PickupHolyWater() => hasHolyWater = true;
    public void PickupCrucifix() => hasCrucifix = true;

    public void UseItems()
    {
        hasHolyWater = false;
        hasCrucifix = false;
    }
}
