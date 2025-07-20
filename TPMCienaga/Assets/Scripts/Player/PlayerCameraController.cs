using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Referencia a la cámara")]
    public Transform cameraTransform;

    [Header("Configuración de sensibilidad")]
    public float sensitivity = 1.5f;
    public float minY = -80f;
    public float maxY = 80f;

    private float rotationY;
    public bool canLook = true; // Control para habilitar/deshabilitar la cámara

    public void ResetVerticalRotation()
    {
        rotationY = 0f;
        cameraTransform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
    }

    public void HandleLook(Vector2 lookInput)
    {
        if (!canLook) return;

        Vector2 mouseDelta = lookInput * sensitivity;

        // Movimiento vertical (Pitch)
        rotationY = Mathf.Clamp(rotationY - mouseDelta.y, minY, maxY);
        cameraTransform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);

        // Movimiento horizontal (Yaw)
        transform.Rotate(Vector3.up * mouseDelta.x);
    }

    public Transform CameraTransform => cameraTransform;
}
