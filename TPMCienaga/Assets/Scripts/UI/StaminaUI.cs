using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public StaminaSystem staminaSystem;
    public Image staminaBar;

    private void Update()
    {
        if (staminaSystem != null && staminaBar != null)
        {
            float percent = staminaSystem.CurrentStamina / staminaSystem.maxStamina;
            staminaBar.fillAmount = percent;

            if (percent > 0.7f)
                staminaBar.color = Color.green;
            else if (percent > 0.3f)
                staminaBar.color = Color.yellow;
            else
                staminaBar.color = Color.red;
        }
    }
}
