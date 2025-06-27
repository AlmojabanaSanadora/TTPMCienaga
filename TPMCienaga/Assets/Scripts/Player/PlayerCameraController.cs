using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float sensitivity = 1.5f;
    public float minY = -80f;
    public float maxY = 80f;

    private float rotationY;

    public void HandleLook(Vector2 lookInput)
    {
        Vector2 mouseDelta = lookInput * sensitivity;

        rotationY = Mathf.Clamp(rotationY - mouseDelta.y, minY, maxY);
        cameraTransform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        transform.Rotate(Vector3.up * mouseDelta.x);
    }
}
