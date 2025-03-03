using UnityEngine;
using TMPro; // Importa el espacio de nombres de TextMeshPro

public class Vida : MonoBehaviour
{
    public float valor = 100;              // Salud máxima del jugador
    public TextMeshProUGUI textoVida;      // Referencia al TextMeshProUGUI de salud

    // Start is called before the first frame update
    void Start()
    {
        if (textoVida != null)
        {
            // Inicializa el texto con el valor de la salud
            textoVida.text = "Salud: " + valor.ToString();
        }
    }

    // Método para recibir daño
    public void RecibirDaño(float daño)
    {
        // Resta el daño de la salud
        valor -= daño;

        // Asegura que la salud no baje de 0
        if (valor < 0)
        {
            valor = 0;
        }

        // Si el TextMeshProUGUI no es nulo, actualizamos el texto
        if (textoVida != null)
        {
            textoVida.text = "Salud: " + valor.ToString();
        }
    }

    // Método opcional para curarse (solo si quieres)
    public void Curarse(float cantidad)
    {
        // Sumar la cantidad de curación
        valor += cantidad;

        // Asegura que la salud no supere el máximo
        if (valor > 100)  // O el valor máximo que quieras
        {
            valor = 100;
        }

        // Actualiza el TextMeshProUGUI con el nuevo valor de la salud
        if (textoVida != null)
        {
            textoVida.text = "Salud: " + valor.ToString();
        }
    }
}

