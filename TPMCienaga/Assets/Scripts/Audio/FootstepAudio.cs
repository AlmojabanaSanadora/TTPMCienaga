using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    public AudioSource footstepSource;
    public float stepInterval = 0.5f;

    public AudioClip[] defaultSteps;
    public AudioClip[] woodSteps;
    public AudioClip[] dirtSteps;
    public AudioClip[] waterSteps;

    private float stepTimer;

    public void HandleFootsteps(bool isMoving, bool isGrounded, Vector3 position)
    {
        if (!isMoving || !isGrounded) return;

        stepTimer -= Time.deltaTime;
        if (stepTimer <= 0f)
        {
            PlayFootstep(position);
            stepTimer = stepInterval;
        }
    }

    private void PlayFootstep(Vector3 pos)
    {
        SurfaceType surface = SurfaceType.Default;

        if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit, 2f))
        {
            SurfaceIdentifier identifier = hit.collider.GetComponent<SurfaceIdentifier>();
            if (identifier != null)
            {
                surface = identifier.surfaceType;
            }
        }

        AudioClip[] selectedClips = defaultSteps;
        switch (surface)
        {
            case SurfaceType.Wood: selectedClips = woodSteps; break;
            case SurfaceType.Dirt: selectedClips = dirtSteps; break;
            case SurfaceType.Water: selectedClips = waterSteps; break;
        }

        if (selectedClips != null && selectedClips.Length > 0)
        {
            AudioClip clip = selectedClips[Random.Range(0, selectedClips.Length)];
            footstepSource.PlayOneShot(clip);
        }
    }
}