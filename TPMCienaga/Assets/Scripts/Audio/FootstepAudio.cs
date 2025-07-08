using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip[] groundSteps;
    public AudioClip[] woodSteps;
    public AudioClip[] waterSteps;
    public AudioClip[] defaultSteps;

    public Transform footOrigin;
    public CharacterController controller;

    public float stepInterval = 0.5f;
    private float stepTimer = 0f;

    private bool wasMoving = false;

    private void Update()
    {
        if (controller == null || footOrigin == null || footstepSource == null) return;

        Vector2 horizontalVelocity = new Vector2(controller.velocity.x, controller.velocity.z);
        bool isMoving = horizontalVelocity.magnitude > 0.1f;
        bool isGrounded = controller.isGrounded;

        if (isMoving && isGrounded)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                SurfaceType surface = GetSurfaceTypeUnderFoot();
                PlayFootstep(surface);
                stepTimer = stepInterval;
            }

            wasMoving = true;
        }
        else if (wasMoving)
        {
            // Al dejar de moverse, reseteamos el timer y estado
            stepTimer = stepInterval;
            wasMoving = false;
        }
    }

    private void PlayFootstep(SurfaceType surface)
    {
        AudioClip[] selectedClips = GetClipsForSurface(surface);

        if (selectedClips != null && selectedClips.Length > 0)
        {
            AudioClip clip = selectedClips[Random.Range(0, selectedClips.Length)];
            footstepSource.PlayOneShot(clip);
        }
    }

    private SurfaceType GetSurfaceTypeUnderFoot()
    {
        Vector3 origin = footOrigin.position + Vector3.up * 0.2f;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 1.5f))
        {
            SurfaceIdentifier identifier = hit.collider.GetComponent<SurfaceIdentifier>();
            if (identifier != null)
            {
                return identifier.surfaceType;
            }
        }

        return SurfaceType.Default;
    }

    private AudioClip[] GetClipsForSurface(SurfaceType surface)
    {
        switch (surface)
        {
            case SurfaceType.Ground: return groundSteps;
            case SurfaceType.Wood: return woodSteps;
            case SurfaceType.Water: return waterSteps;
            default: return defaultSteps;
        }
    }
}
