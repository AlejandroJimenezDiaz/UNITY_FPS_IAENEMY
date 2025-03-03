using UnityEngine;
using UnityEngine.UI;

public class JugadorSalud : MonoBehaviour
{
    public float saludMaxima = 100f;
    public float saludActual;
    public Slider barraSalud; // Referencia al Slider

    void Start()
    {
        saludActual = saludMaxima;
        if (barraSalud != null)
        {
            barraSalud.maxValue = saludMaxima;
            barraSalud.value = saludActual;
        }
    }

    public void RecibirDaño(float daño)
    {
        saludActual -= daño;
        if (saludActual < 0) saludActual = 0;
        if (barraSalud != null)
            barraSalud.value = saludActual;

        // Si la salud llega a 0, el jugador muere
        if (saludActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        // Lógica de muerte del jugador
        Debug.Log("El jugador ha muerto");
        // Aquí puedes llamar a otras funciones, como reiniciar la escena o mostrar un mensaje de derrota
    }
}
