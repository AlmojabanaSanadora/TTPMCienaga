using UnityEngine;

public class ClosetCameraLook : MonoBehaviour
{
    public float sensitivity = 2f;
    public float clampAngle = 60f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private bool isActive = false;

    public void ActivateCameraLook()
    {
        isActive = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DeactivateCameraLook()
    {
        isActive = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (!isActive) return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX += mouseX;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -clampAngle, clampAngle);
        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}
