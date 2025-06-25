using UnityEngine;

public class HolyWaterFountain : MonoBehaviour
{
    public GameObject pickupPromptUI;
    private bool playerInRange = false;
    private PlayerInventory playerInventory;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.B) && playerInventory.CanPickupHolyWater() && !playerInventory.IsInventoryOpen())
        {
            playerInventory.PickupHolyWater();
            Debug.Log("Agua bendita recogida.");

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

            if (pickupPromptUI != null && playerInventory.CanPickupHolyWater())
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
