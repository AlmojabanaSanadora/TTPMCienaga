using UnityEngine;
using TMPro;

public class NotesUI : MonoBehaviour
{
    public GameObject pickupPromptUI;
    public GameObject noteUI; 
    public TextMeshProUGUI noteUIText; 
    public TextMeshProUGUI noteUIText2;
    public TextMeshProUGUI noteUITitle1;
    public TextMeshProUGUI noteUITitle2;
    public string noteText;
    public string noteText2;
    public string noteTitle1;
    public string noteTitle2;
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

        if (noteUI.activeSelf && Input.GetKeyDown(KeyCode.G))
        {
            HideNote();
        }
    }

    private void PickUpNote()
    {
        Vector3 notePosition = playerTransform.position + playerTransform.forward * noteDistance + Vector3.up * noteHeight;
        Quaternion noteRotation = Quaternion.LookRotation(-playerTransform.forward);

        switch (gameObject.name)
        {
            case "nota1":
                noteRotation = Quaternion.Euler(-180f, 0f, -180f); 
                break;
            case "nota2":
                noteRotation *= Quaternion.Euler(90f, 0f, 0f); 
                break;
            case "nota3":
                noteRotation *= Quaternion.Euler(90f, 0f, 0f); 
                break;
            case "nota4":
                noteRotation *= Quaternion.Euler(90f, 0f, 0f); 
                break;
            case "nota5":
                noteRotation *= Quaternion.Euler(90f, 0f, 0f); 
                break;
            case "nota6":
                noteRotation *= Quaternion.Euler(90f, 0f, 0f); 
                break;
            case "nota7":
                noteRotation *= Quaternion.Euler(90f, 0f, 0f);  
                break;
            case "nota8":
                noteRotation *= Quaternion.Euler(0f, 0f, 0f);  
                break;
            case "nota9":
                noteRotation *= Quaternion.Euler(-180f, -180f, 180f); 
                break;     
            case "nota10":
                noteRotation *= Quaternion.Euler(-180f, -180f, 180f); 
                break;
            case "nota11":
                noteRotation *= Quaternion.Euler(0f, 0f, 0f); 
                break;
            case "nota12":
                noteRotation *= Quaternion.Euler(-180f, 0, 180f); 
                break;        
            default:
                break;
        }

        transform.position = notePosition;
        transform.rotation = noteRotation;

        noteUI.SetActive(true);
        noteUIText.text = noteText;
        noteUIText2.text = noteText2;
        noteUITitle1.text = noteTitle1;
        noteUITitle2.text = noteTitle2;

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

        Debug.Log($"Destroying note: {this.gameObject.name}");
            Destroy(this.gameObject);
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