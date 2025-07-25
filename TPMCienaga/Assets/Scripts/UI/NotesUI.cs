using UnityEngine;
using UnityEngine.UI;

public class NotesUI : MonoBehaviour
{
    public Sprite imagenNota; // Imagen que se mostrará al leer
    public GameObject canvasNota; // Canvas general
    public Image imagenUI; // El componente Image donde se verá la nota
    public GameObject textoFlotante; // Texto que dice "Presiona X para recoger"

    private bool jugadorCerca = false;
    private bool leyendo = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.X))
        {
            if (!leyendo)
            {
                AbrirNota();
            }
            else
            {
                CerrarNota();
            }
        }
    }

    void AbrirNota()
    {
        imagenUI.sprite = imagenNota;
        canvasNota.SetActive(true);
        textoFlotante.SetActive(false); // Ocultar texto flotante al leer
        leyendo = true;
        Time.timeScale = 0f; // Pausar juego
    }

    void CerrarNota()
    {
        canvasNota.SetActive(false);
        leyendo = false;
        Time.timeScale = 1f; // Reanudar juego
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            textoFlotante.SetActive(true); // Mostrar el texto
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            textoFlotante.SetActive(false); // Ocultar el texto
            if (!leyendo)
                canvasNota.SetActive(false);
        }
    }
}
