using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private GameObject Hurt1UI;
    [SerializeField] private GameObject Hurt2UI;

    private bool isDead = false;

    private void Update()
    {
        if (!isDead && health <= 0f)
        {
            HandleDeath();
        }

        UpdateUIBasedOnHealth();
    }

    private void HandleDeath()
    {
        isDead = true;
        SceneManager.LoadScene("GameOver");
        // Opcional: gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        health = Mathf.Max(health, 0f);
    }

    private void UpdateUIBasedOnHealth()
    {
        if (health <= 67f && health > 33f)
        {
            Hurt1UI?.SetActive(true);
            Hurt2UI?.SetActive(false);
        }
        else if (health <= 33f)
        {
            Hurt1UI?.SetActive(false);
            Hurt2UI?.SetActive(true);
        }
        else
        {
            Hurt1UI?.SetActive(false);
            Hurt2UI?.SetActive(false);
        }
    }
}
