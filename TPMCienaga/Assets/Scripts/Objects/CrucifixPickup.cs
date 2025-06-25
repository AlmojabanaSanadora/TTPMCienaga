using UnityEngine;

public class CrucifixPickup : MonoBehaviour
{
    public GameObject pickupPromptUI;

    private PlayerInventory playerInventory;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.B) && playerInventory.CanPickupCrucifix() && !playerInventory.IsInventoryOpen())
        {
            playerInventory.PickupCrucifix();
            Destroy(gameObject);

            if (pickupPromptUI != null)
                pickupPromptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();
            playerInRange = true;

            if (pickupPromptUI != null && playerInventory.CanPickupCrucifix())
                pickupPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerInventory = null;

            if (pickupPromptUI != null)
                pickupPromptUI.SetActive(false);
        }
    }
}
