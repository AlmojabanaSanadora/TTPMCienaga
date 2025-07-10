using UnityEngine;

public class PlayerHidingHandler : MonoBehaviour
{
    public Transform hidePosition;
    public Transform unhidePosition;
    public Transform hideLookDirection;

    public KeyCode hideKey = KeyCode.E;

    public GameObject playerController;
    public AudioSource audioSource;
    public AudioClip hideSound;
    public AudioClip unhideSound;

    private bool isHiding = false;

    // Referencias a los scripts que controlan al jugador
    private PlayerInputManager inputManager;
    private PlayerMovement movement;
    private PlayerCameraController cameraController;
    private FootstepAudio footstepAudio;
    private CharacterController characterController;

    private void Start()
    {
        if (playerController != null)
        {
            inputManager = playerController.GetComponent<PlayerInputManager>();
            movement = playerController.GetComponent<PlayerMovement>();
            cameraController = playerController.GetComponent<PlayerCameraController>();
            footstepAudio = playerController.GetComponent<FootstepAudio>();
            characterController = playerController.GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(hideKey))
        {
            if (!isHiding)
                HidePlayer();
            else
                UnhidePlayer();
        }
    }

    private void HidePlayer()
    {
        isHiding = true;

        // Mueve al jugador a la posición de ocultarse
        transform.position = hidePosition.position;

        // Rotación horizontal hacia la dirección deseada
        if (hideLookDirection != null)
        {
            Vector3 flatForward = hideLookDirection.forward;
            flatForward.y = 0f;
            if (flatForward != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(flatForward);
        }
        else
        {
            Vector3 flatForward = hidePosition.forward;
            flatForward.y = 0f;
            transform.rotation = Quaternion.LookRotation(flatForward);
        }

        SetPlayerControl(false);

        if (cameraController != null)
            cameraController.ResetVerticalRotation(); // NUEVO: resetea mirada vertical

        if (audioSource != null && hideSound != null)
            audioSource.PlayOneShot(hideSound);
    }

    private void UnhidePlayer()
    {
        isHiding = false;

        transform.position = unhidePosition.position;
        transform.rotation = Quaternion.Euler(0f, unhidePosition.eulerAngles.y, 0f);

        SetPlayerControl(true);

        if (audioSource != null && unhideSound != null)
            audioSource.PlayOneShot(unhideSound);
    }

    private void SetPlayerControl(bool enabled)
    {
        if (inputManager != null) inputManager.enabled = enabled;
        if (movement != null) movement.enabled = enabled;
        if (cameraController != null) cameraController.enabled = enabled;
        if (footstepAudio != null) footstepAudio.enabled = enabled;
        if (characterController != null) characterController.enabled = enabled;
    }

    public bool IsHiding()
    {
        return isHiding;
    }
}
