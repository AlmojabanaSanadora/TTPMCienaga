using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public StaminaSystem staminaSystem;
    public Image staminaBar;

    // Colores tenues personalizados con transparencia
    public Color midStaminaColor = new Color(0.76f, 0.28f, 0.04f, 0.6f); // Naranja (#C1470A, transparente)
    public Color lowStaminaColor = new Color(0.4f, 0f, 0f, 0.5f);        // Rojo oscuro, también con transparencia

    private Color initialColor;

    private void Start()
    {
        // Guardamos el color original del Canvas (alto porcentaje)
        if (staminaBar != null)
            initialColor = staminaBar.color;
    }

    private void Update()
    {
        if (staminaSystem != null && staminaBar != null)
        {
            float percent = staminaSystem.CurrentStamina / staminaSystem.maxStamina;
            staminaBar.fillAmount = percent;

            if (percent > 0.7f)
                staminaBar.color = initialColor;
            else if (percent > 0.3f)
                staminaBar.color = midStaminaColor;
            else
                staminaBar.color = lowStaminaColor;
        }
    }
}
