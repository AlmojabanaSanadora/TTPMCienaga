using UnityEngine;

public class CrouchHandler : MonoBehaviour
{
    public CharacterController controller;
    public float crouchHeight = 1.2f;         
    public float standHeight = 2f;
    public Camera playerCamera;
    public float crouchFOV = 65f;             
    public float defaultFOV = 60f;

    public bool IsCrouching { get; private set; }

    private float originalCameraY;
    private Color originalFogColor;
    private float originalFogDensity;

    private void Start()
    {
        originalCameraY = playerCamera.transform.localPosition.y;
        originalFogColor = RenderSettings.fogColor;
        originalFogDensity = RenderSettings.fogDensity;
    }

    public void ToggleCrouch()
    {
        IsCrouching = !IsCrouching;

        controller.height = IsCrouching ? crouchHeight : standHeight;

        Vector3 camPos = playerCamera.transform.localPosition;
        camPos.y = IsCrouching ? originalCameraY - 0.3f : originalCameraY; 
        playerCamera.transform.localPosition = camPos;

        if (IsCrouching)
        {
            RenderSettings.fogColor = new Color(0.05f, 0.05f, 0.05f);
            RenderSettings.fogDensity = 0.35f;
            playerCamera.fieldOfView = crouchFOV;
        }
        else
        {
            RenderSettings.fogColor = originalFogColor;
            RenderSettings.fogDensity = originalFogDensity;
            playerCamera.fieldOfView = defaultFOV;
        }
    }
}
