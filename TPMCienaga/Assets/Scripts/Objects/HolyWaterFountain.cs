using UnityEngine;

public class HolyWaterFountain : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.CanPickupHolyWater())
            {
                inventory.PickupHolyWater();
                Debug.Log("Agua bendita recogida.");
            }
        }
    }
}

