using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public DemonAI demon;
    public PlayerInventory playerInventory;
    public GameObject interactPromptUI;
    public Text warningTextUI;

    private bool playerInRange = false;
    private bool destroyed = false;

    private void Update()
    {
        if (!playerInRange || destroyed) return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (playerInventory.hasHolyWater && playerInventory.hasCrucifix)
            {
                playerInventory.UseItems();
                DestroyPortal();
            }
            else
            {
                ShowMissingItemsMessage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyed) return;

        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();
            playerInRange = true;

            if (interactPromptUI != null)
                interactPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactPromptUI != null)
                interactPromptUI.SetActive(false);

            if (warningTextUI != null)
                warningTextUI.text = "";
        }
    }

    private void DestroyPortal()
    {
        destroyed = true;
        if (warningTextUI != null)
            warningTextUI.text = "";

        demon.IncreaseAggression();
        Destroy(gameObject);
    }

    private void ShowMissingItemsMessage()
    {
        if (warningTextUI == null) return;

        bool hasWater = playerInventory.hasHolyWater;
        bool hasCrucifix = playerInventory.hasCrucifix;

        if (!hasWater && !hasCrucifix)
            warningTextUI.text = "Falta agua bendita y crucifijo.";
        else if (!hasWater)
            warningTextUI.text = "Falta agua bendita.";
        else if (!hasCrucifix)
            warningTextUI.text = "Falta crucifijo.";
    }
}
