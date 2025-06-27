using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 20f;
    public float recoveryTime = 22f;
    public float CurrentStamina { get; private set; }

    public bool CanSprint => CurrentStamina > 0f;

    private void Start()
    {
        CurrentStamina = maxStamina;
    }

    public void HandleStamina(bool isSprinting)
    {
        if (isSprinting)
        {
            CurrentStamina -= Time.deltaTime;
        }
        else
        {
            float recoveryRate = maxStamina / recoveryTime;
            CurrentStamina += recoveryRate * Time.deltaTime;
        }

        CurrentStamina = Mathf.Clamp(CurrentStamina, 0f, maxStamina);
    }
}