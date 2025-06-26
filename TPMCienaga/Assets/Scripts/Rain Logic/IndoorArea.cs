using UnityEngine;

public class IndoorArea : MonoBehaviour
{
    public GameObject rainEffect; // arrastra aquí el sistema de lluvia en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rainEffect.SetActive(false); // Al entrar a la casa
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rainEffect.SetActive(true); // Al salir de la casa
        }
    }
}
