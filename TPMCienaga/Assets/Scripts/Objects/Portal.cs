using UnityEngine;

public class Portal : MonoBehaviour
{
    public DemonAI demon;
    public PlayerInventory playerInventory;
    private bool destroyed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (destroyed) return;

        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.hasHolyWater && inventory.hasCrucifix)
            {
                inventory.UseItems();
                DestroyPortal();
            }
            else
            {
                // Puedes mostrar un mensaje de advertencia aquí
                Debug.Log("Falta agua bendita o crucifijo de madera.");
            }
        }
    }

    private void DestroyPortal()
    {
        destroyed = true;
        demon.IncreaseAggression();
        Destroy(gameObject);
    }
}
