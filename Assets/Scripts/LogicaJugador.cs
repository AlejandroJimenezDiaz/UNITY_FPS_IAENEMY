using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour 
{
    public Vida vida;
    public bool Vida0 = false;
    [SerializeField] private Animator animadorPerder; 

    void Start() 
    {
        vida = GetComponent<Vida>();
        
        if (vida == null) 
        {
            Debug.LogError("❌ El jugador no tiene el componente Vida asignado.");
        }

        if (animadorPerder == null) 
        {
            Debug.LogWarning("⚠️ No se asignó el Animator de 'Perder'. La animación no se ejecutará.");
        }
    }
    
    void Update() 
    {
        RevisarVida();
    }

    void RevisarVida()
    {
        if (Vida0) return;
        
        if (vida.valor <= 0)
        {
            Vida0 = true;

            if (animadorPerder != null)
            {
                animadorPerder.SetTrigger("Perder");
            }

            Invoke("ReiniciarJuego", 3f); // Se da más tiempo para ver la animación
        }
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
