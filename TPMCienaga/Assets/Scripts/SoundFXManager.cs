using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public AudioClip ambientClip;
    public AudioClip proximityClip;
    public Transform player;
    public Transform enemy;

    public float detectionRadius;
    public float fadeSpeed = 4f;
    private AudioSource ambientSource;
    private AudioSource proximitySource;    

    void Start()
    {
        ambientSource = gameObject.AddComponent<AudioSource>();
        if (ambientClip != null)
        {
            ambientSource.clip = ambientClip;
            ambientSource.loop = true;
            ambientSource.volume = 1f; 
            ambientSource.Play();
        }

        proximitySource = gameObject.AddComponent<AudioSource>();
        if (proximityClip != null)
        {
            proximitySource.clip = proximityClip;
            proximitySource.loop = true; 
            proximitySource.volume = 0f; 
        }
    }

    void Update()
    {
        if (player != null && enemy != null && proximityClip != null)
        {
            float distance = Vector3.Distance(player.position, enemy.position);

            if (distance <= detectionRadius)
            {
                proximitySource.volume = Mathf.Lerp(proximitySource.volume, 1f, Time.deltaTime * fadeSpeed);
                ambientSource.volume = Mathf.Lerp(ambientSource.volume, 0.2f, Time.deltaTime * fadeSpeed);

                if (!proximitySource.isPlaying)
                {
                    proximitySource.Play();
                }
            }
            else
            {
                proximitySource.volume = Mathf.Lerp(proximitySource.volume, 0f, Time.deltaTime * fadeSpeed);
                ambientSource.volume = Mathf.Lerp(ambientSource.volume, 1f, Time.deltaTime * fadeSpeed);

                if (proximitySource.volume <= 0.01f && proximitySource.isPlaying)
                {
                    proximitySource.Stop();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player != null && enemy != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(player.position, detectionRadius);
        }
    }
} 
