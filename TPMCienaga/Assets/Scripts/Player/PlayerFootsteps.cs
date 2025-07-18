using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Footstep Settings")]
    public LayerMask groundMask;
    public float rayDistance = 9f; // Aumentado según tu solicitud
    public float stepInterval = 0.5f;

    [Header("Audio")]
    public AudioSource audioSource;
    public FootstepData[] footstepDatas;

    private float stepTimer;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource no asignado en PlayerFootsteps.");
        }
    }

    void Update()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        // Ray desde el centro del jugador hacia abajo
        Ray ray = new Ray(transform.position + Vector3.up * 0.2f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, groundMask))
        {
            string hitLayerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
            Debug.Log("Hit suelo: " + hitLayerName);

            foreach (var data in footstepDatas)
            {
                if (data.layerName == hitLayerName && data.footstepClips.Length > 0)
                {
                    int index = Random.Range(0, data.footstepClips.Length);
                    audioSource.clip = data.footstepClips[index];
                    audioSource.pitch = Random.Range(0.95f, 1.05f);
                    audioSource.Play();
                    return;
                }
            }

            Debug.LogWarning("No se encontraron sonidos para la capa: " + hitLayerName);
        }
        else
        {
            Debug.LogWarning("Raycast no tocó ningún suelo en el LayerMask.");
        }
    }
}
