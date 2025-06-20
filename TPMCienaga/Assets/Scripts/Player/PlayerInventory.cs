using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasHolyWater = false;
    public bool hasCrucifix = false;

    public InventoryUIManager uiManager;      // Arrastra el UIManager aquí en el Inspector
    public GameObject pickupPromptUI;         // Arrastra el texto "Presiona B para recoger" aquí

    private CrucifixPickup nearbyCrucifix;

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
        // Presionar B para recoger el crucifijo si estás cerca y no tienes uno
        if (Input.GetKeyDown(KeyCode.B) && nearbyCrucifix != null && CanPickupCrucifix())
        {
            PickupCrucifix();
            Destroy(nearbyCrucifix.gameObject);
            nearbyCrucifix = null;

            if (pickupPromptUI != null)
                pickupPromptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crucifix"))
        {
            nearbyCrucifix = other.GetComponent<CrucifixPickup>();
            if (!hasCrucifix && pickupPromptUI != null)
            {
                pickupPromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crucifix"))
        {
            if (nearbyCrucifix != null && other.gameObject == nearbyCrucifix.gameObject)
            {
                nearbyCrucifix = null;
                if (pickupPromptUI != null)
                {
                    pickupPromptUI.SetActive(false);
                }
            }
        }
    }
}
