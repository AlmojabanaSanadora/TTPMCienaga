using UnityEngine;
using UnityEngine.UI;

public class ControlInstructions : MonoBehaviour
{
    public GameObject panelControles;       // Panel UI con la imagen de controles
    public MonoBehaviour movimientoJugador; // Script de movimiento del jugador
    public MonoBehaviour miradaJugador;     // Script de control de cámara
    public Button botonComenzar;            // Botón "Comenzar"

    private void Start()
    {
        // Mostrar el panel y pausar movimiento
        panelControles.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        movimientoJugador.enabled = false;
        miradaJugador.enabled = false;

        // Configurar botón
        botonComenzar.onClick.AddListener(CerrarPanel);
    }

    void CerrarPanel()
    {
        // Ocultar panel y reanudar control
        panelControles.SetActive(false);
        Time.timeScale = 1f;

        movimientoJugador.enabled = true;
        miradaJugador.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
