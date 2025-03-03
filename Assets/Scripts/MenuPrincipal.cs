using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject menuCanvas; // Referencia al Canvas del menú principal

    // Función para iniciar el juego
    public void IniciarJuego()
    {


        // Cambia a la escena de juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    // Función para salir del juego
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}

