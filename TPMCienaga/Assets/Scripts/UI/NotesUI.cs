using UnityEngine;
using TMPro;

public class NotesUI : MonoBehaviour
{
    public GameObject pickupPromptUI;
    public GameObject noteUI; 
    public TextMeshProUGUI noteUIText; 
    public string noteText;
    public float noteDistance = 2f; 
    public float noteHeight = 1f; 
    public GameObject lantern; 

    private bool playerInRange = false;
    private Transform playerTransform;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.X)) 
        {
            PickUpNote();
        }

        // Allow closing the note by pressing 'G'
        if (noteUI.activeSelf && Input.GetKeyDown(KeyCode.G)) 
        {
            HideNote();
        }
    }

    private void PickUpNote()
    {
        Vector3 notePosition = playerTransform.position - playerTransform.forward * noteDistance + Vector3.up * noteHeight;
        Quaternion noteRotation = Quaternion.LookRotation(playerTransform.forward);

        transform.position = notePosition;
        transform.rotation = noteRotation;

        noteUI.SetActive(true);
        noteUIText.text = noteText;

        if (pickupPromptUI != null)
            pickupPromptUI.SetActive(false);

        if (lantern != null)
            lantern.SetActive(false);

        Time.timeScale = 0f; 
    }

    public void HideNote()
    {
        noteUI.SetActive(false);
        Time.timeScale = 1f; 

        if (lantern != null)
            lantern.SetActive(true);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerTransform = other.transform; 

            if (pickupPromptUI != null)
                pickupPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerTransform = null; 

            if (pickupPromptUI != null)
                pickupPromptUI.SetActive(false);
        }
    }
}