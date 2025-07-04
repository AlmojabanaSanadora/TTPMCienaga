using UnityEngine;


public class FootstepAudio : MonoBehaviour
{
    public AudioSource footstepSource;
    public float stepInterval = 0.5f;

    public AudioClip[] groundSteps;
    public AudioClip[] woodSteps;
    public AudioClip[] waterSteps;
    public AudioClip[] defaultSteps;

    private float stepTimer;
    private SurfaceType lastSurface = SurfaceType.Default;

    public void HandleFootsteps(bool isMoving, bool isGrounded, Vector3 position)
    {
        if (!isMoving || !isGrounded)
        {
            stepTimer = 0f; // Reset timer para evitar bug de sonido después
            return;
        }

        stepTimer -= Time.deltaTime;

        if (stepTimer <= 0f)
        {
            PlayFootstep(position);
            stepTimer = stepInterval;
        }
    }

    private void PlayFootstep(Vector3 pos)
    {
        SurfaceType surface = GetSurfaceTypeUnderFoot(pos);

        AudioClip[] selectedClips = GetClipsForSurface(surface);

        if (selectedClips != null && selectedClips.Length > 0)
        {
            AudioClip clip = selectedClips[Random.Range(0, selectedClips.Length)];
            footstepSource.PlayOneShot(clip);
        }
    }

    private SurfaceType GetSurfaceTypeUnderFoot(Vector3 pos)
    {
        if (Physics.Raycast(pos + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 3f))
        {
            SurfaceIdentifier identifier = hit.collider.GetComponent<SurfaceIdentifier>();
            if (identifier != null)
                return identifier.surfaceType;
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
