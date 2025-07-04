using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public Transform cameraTransform;
    public float bobFrequency = 7f;
    public float bobAmplitude = 0.035f;
    public float sprintMultiplier = 1.5f;
    public float tiltAmount = 1.25f;
    public float smoothSpeed = 10f;

    private Vector3 initialPos;
    private float bobTimer;

    private Vector3 currentBobOffset;
    private Vector3 bobVelocity;
    private float currentTiltZ;
    private float tiltVelocity;

    private void Start()
    {
        if (cameraTransform != null)
            initialPos = cameraTransform.localPosition;

        bobTimer = Random.Range(0f, Mathf.PI * 2f);
    }

    public void HandleHeadBob(Vector2 moveInput, bool isGrounded, bool isSprinting)
    {
        bool isMoving = moveInput.magnitude > 0.1f;
        bool shouldBob = isMoving && isGrounded;

        if (!shouldBob)
        {
            currentBobOffset = Vector3.SmoothDamp(currentBobOffset, Vector3.zero, ref bobVelocity, 0.1f);
            cameraTransform.localPosition = initialPos + currentBobOffset;

            currentTiltZ = Mathf.SmoothDamp(currentTiltZ, 0f, ref tiltVelocity, 0.1f);
            ApplyTilt(currentTiltZ);
            return;
        }

        float freq = bobFrequency * (isSprinting ? sprintMultiplier : 1f);
        bobTimer += Time.deltaTime * freq;

        float offsetY = Mathf.Sin(bobTimer) * bobAmplitude;
        float offsetX = Mathf.Sin(bobTimer * 0.5f) * bobAmplitude * 0.6f;
        float tiltZ = Mathf.Sin(bobTimer * 0.5f) * tiltAmount;

        Vector3 targetOffset = new Vector3(offsetX, offsetY, 0f);
        currentBobOffset = Vector3.SmoothDamp(currentBobOffset, targetOffset, ref bobVelocity, 0.05f);
        cameraTransform.localPosition = initialPos + currentBobOffset;

        currentTiltZ = Mathf.SmoothDamp(currentTiltZ, tiltZ, ref tiltVelocity, 0.05f);
        ApplyTilt(currentTiltZ);
    }

    private void ApplyTilt(float tiltZ)
    {
        Vector3 angles = cameraTransform.localEulerAngles;
        angles.z = tiltZ;
        cameraTransform.localEulerAngles = angles;
    }
}
