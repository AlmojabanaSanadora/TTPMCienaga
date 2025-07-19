using UnityEngine;

public class WoodFootstepZone : MonoBehaviour
{
    public AudioClip woodFootstepClip;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null && woodFootstepClip != null)
        {
            player.SetTemporaryFootstep(woodFootstepClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.ResetFootstep();
        }
    }
}
