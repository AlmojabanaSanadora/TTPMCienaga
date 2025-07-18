using UnityEngine;

public class ClosetHidingZone : MonoBehaviour
{
    public Transform hidePosition;
    public Transform unhidePosition;
    public Transform hideLookDirection;

    public AudioSource audioSource;
    public AudioClip hideSound;
    public AudioClip unhideSound;

    public MonoBehaviour cameraRotationWhileHidden;

    private GameObject player;
    private bool isPlayerInZone = false;
    private bool isPlayerHiding = false;

    private void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            if (!isPlayerHiding)
                HidePlayer();
            else
                UnhidePlayer();
        }
    }

    private void HidePlayer()
    {
        isPlayerHiding = true;
        GameState.PlayerIsHiding = true;

        if (player == null) return;

        var cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = hidePosition.position;
        Vector3 lookDir = (hideLookDirection != null) ? hideLookDirection.forward : hidePosition.forward;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
            player.transform.rotation = Quaternion.LookRotation(lookDir);

        // Desactiva solo el movimiento
        SetPlayerComponents(enableMovement: false);

        if (cameraRotationWhileHidden != null)
            cameraRotationWhileHidden.enabled = true;

        if (audioSource != null && hideSound != null)
            audioSource.PlayOneShot(hideSound);
    }

    private void UnhidePlayer()
    {
        isPlayerHiding = false;
        GameState.PlayerIsHiding = false;

        if (player == null) return;

        player.transform.position = unhidePosition.position;
        player.transform.rotation = Quaternion.Euler(0f, unhidePosition.eulerAngles.y, 0f);

        SetPlayerComponents(enableMovement: true);

        if (cameraRotationWhileHidden != null)
            cameraRotationWhileHidden.enabled = false;

        if (audioSource != null && unhideSound != null)
            audioSource.PlayOneShot(unhideSound);

        var cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = true;
    }

    private void SetPlayerComponents(bool enableMovement)
    {
        var inputManager = player.GetComponent<PlayerInputManager>();
        if (inputManager != null) inputManager.enabled = enableMovement;

        var movement = player.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = enableMovement;

        // Siempre dejar cámara activa
        var cameraController = player.GetComponent<PlayerCameraController>();
        if (cameraController != null) cameraController.enabled = true;

        var footsteps = player.GetComponent<FootstepAudio>();
        if (footsteps != null) footsteps.enabled = enableMovement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            if (!isPlayerHiding) player = null;
        }
    }
}
