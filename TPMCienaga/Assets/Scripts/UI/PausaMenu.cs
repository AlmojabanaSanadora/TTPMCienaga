using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaMenu : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa = false;
    public GameObject MenuSalir;

    private PlayerCameraController cameraController;

    void Start()
    {
        // Encuentra el script de la cámara automáticamente
        cameraController = Object.FindFirstObjectByType<PlayerCameraController>();

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
