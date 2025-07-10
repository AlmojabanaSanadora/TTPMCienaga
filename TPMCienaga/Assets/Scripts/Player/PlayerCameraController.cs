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

    public void ResetVerticalRotation()
    {
        rotationY = 0f;
        cameraTransform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
    }


    public void HandleLook(Vector2 lookInput)
    {
        Vector2 mouseDelta = lookInput * sensitivity;

        // Movimiento vertical (Pitch)
        rotationY = Mathf.Clamp(rotationY - mouseDelta.y, minY, maxY);
        cameraTransform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);

        // Movimiento horizontal (Yaw)
        transform.Rotate(Vector3.up * mouseDelta.x);
    }

    // Por si quieres acceder con propiedad también
    public Transform CameraTransform => cameraTransform;
}
