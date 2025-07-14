using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public void EmpezarJuego(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego.");
    }
}
