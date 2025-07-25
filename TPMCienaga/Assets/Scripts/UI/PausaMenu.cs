using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaMenu : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;
    public GameObject MenuSalir;

    private PlayerCameraController cameraController;
    private Canvas canvasPausa;

    void Start()
    {
        cameraController = Object.FindFirstObjectByType<PlayerCameraController>();

        // Obtener el Canvas del menú de pausa (debe estar en el mismo objeto o en un hijo)
        canvasPausa = ObjetoMenuPausa.GetComponent<Canvas>();
        if (canvasPausa != null)
        {
            canvasPausa.overrideSorting = true;
            canvasPausa.sortingOrder = 100; // Mayor a cualquier otro
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Pausa)
            {
                ActivarPausa();
            }
            else
            {
                Resumir();
            }
        }
    }

    public void ActivarPausa()
    {
        if (canvasPausa != null)
        {
            canvasPausa.sortingOrder = 100; // Asegurarse de estar encima
        }

        ObjetoMenuPausa.SetActive(true);
        MenuSalir.SetActive(false);
        Pausa = true;

        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (cameraController != null)
            cameraController.canLook = false;
    }

    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (cameraController != null)
            cameraController.canLook = true;
    }

    public void MenuPrincipal(string NombreMenu)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(NombreMenu);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
