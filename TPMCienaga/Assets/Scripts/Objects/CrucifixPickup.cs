using UnityEngine;

public class CrucifixPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.CanPickupCrucifix())
            {
                inventory.PickupCrucifix();
                Destroy(gameObject);
            }
        }
    }
}
