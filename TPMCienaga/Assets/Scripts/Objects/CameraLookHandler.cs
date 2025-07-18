using UnityEngine;

public class CameraLookHandler : MonoBehaviour
{
    public PlayerCameraController cameraController;
    public PlayerInputManager inputManager;

    void Update()
    {
        if (cameraController != null && inputManager != null)
        {
            cameraController.HandleLook(inputManager.LookInput);
        }
    }
}
