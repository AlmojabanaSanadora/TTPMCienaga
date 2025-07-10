using UnityEngine;

public class HideSpot : MonoBehaviour
{
    public Transform hidePosition;
    public bool IsPlayerHidingHere { get; private set; }

    private void OnDrawGizmos()
    {
        if (hidePosition != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(hidePosition.position, 0.1f);
        }
    }
}