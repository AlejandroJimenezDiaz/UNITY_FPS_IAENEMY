using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModoDeDisparo
{
    SemiAuto,
    FullAuto
}

public class LogicaArma : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;
    public bool tiempoNoDisparo = false;
    public bool puedeDisparar = false;
    public bool recargando = false;

    [Header("Referencia de objetos")]
    public ParticleSystem fuegoDeArma;
        public Transform puntoDeDisparo;

    [Header("Referencia de Sonidos")]
    public AudioClip sonDisparo;
    public AudioClip SonSinBalas;
    public AudioClip SonCartuEntra;
    public AudioClip SonCartuSale;
    public AudioClip sonidoDeDesenfundar;
    public AudioClip sonidoVacio;

    [Header("Atributos de Arma")]
    public ModoDeDisparo modoDeDisparo = ModoDeDisparo.FullAuto;
    public float daño = 20f;
    public float ritmoDeDisparo = 0.3f;
    public int balasRestantes;
    public int balasEnCartucho;
    public int tamañoDeCartucho = 12;
    public int maximoDeBalas = 100;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        balasEnCartucho = tamañoDeCartucho;
        balasRestantes = maximoDeBalas;
        Invoke("HabilitarArma", 0.5f);
    }

    void Update()
    {
        if (modoDeDisparo == ModoDeDisparo.FullAuto && Input.GetButton("Fire1"))
        {
            RevisarDisparo();
        }
        else if (modoDeDisparo == ModoDeDisparo.SemiAuto && Input.GetButtonDown("Fire1"))
        {
            RevisarDisparo();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RevisarRecargar();
        }
    }

    void HabilitarArma()
    {
        puedeDisparar = true;
    }

    void RevisarDisparo()
    {
        if (!puedeDisparar || tiempoNoDisparo || recargando) return;

        if (balasEnCartucho > 0)
        {
            Disparar();
        }
        else
        {
            SinBalas();
        }
    }

    void Disparar()
    {
        audioSource.PlayOneShot(sonDisparo);
        tiempoNoDisparo = true;
        fuegoDeArma.Stop();
        fuegoDeArma.Play();
        ReproducirAnimacionDisparo();
        balasEnCartucho--;
        DisparoDirecto();
        StartCoroutine(ReiniciarTiempoNoDisparo());
    }

    void DisparoDirecto()
    {
        RaycastHit hit;
        if(Physics.Raycast(puntoDeDisparo.position, puntoDeDisparo.forward, out hit))
        {
            if (hit.transform.CompareTag("Enemigo"))
            {
                Vida vida = hit.transform.GetComponent<Vida>();
                if(vida == null)
                {
                    throw new System.Exception("No se encontro el componente Vida del Enemigo");
                }
                else
                {
                    vida.RecibirDaño(daño);
                }
            }
        }
    }
    public virtual void ReproducirAnimacionDisparo()
    {
        if (gameObject.name == "Police9mm")
        {
            animator.CrossFadeInFixedTime(balasEnCartucho > 0 ? "Fire" : "FireLast", 0.1f);
        }
        else
        {
            animator.CrossFadeInFixedTime("Fire", 0.1f);
        }
    }

    void SinBalas()
    {
        audioSource.PlayOneShot(SonSinBalas);
        tiempoNoDisparo = true;
        StartCoroutine(ReiniciarTiempoNoDisparo());
    }

    IEnumerator ReiniciarTiempoNoDisparo()
    {
        yield return new WaitForSeconds(ritmoDeDisparo);
        tiempoNoDisparo = false;
    }

    void RevisarRecargar()
    {
        if (!recargando && balasRestantes > 0 && balasEnCartucho < tamañoDeCartucho)
        {
            Recargar();
        }
    }

    void Recargar()
    {
        recargando = true;
        animator.CrossFadeInFixedTime("Reload", 0.1f);
        audioSource.PlayOneShot(SonCartuSale);
        StartCoroutine(FinalizarRecarga());
    }

    IEnumerator FinalizarRecarga()
    {
        yield return new WaitForSeconds(1.5f); // Tiempo de recarga según la animación
        RecargarMuniciones();
        recargando = false;
        audioSource.PlayOneShot(SonCartuEntra);
    }

    void RecargarMuniciones()
    {
        int balasParaRecargar = tamañoDeCartucho - balasEnCartucho;
        int balasUsadas = Mathf.Min(balasRestantes, balasParaRecargar);

        balasRestantes -= balasUsadas;
        balasEnCartucho += balasUsadas;
    }

    public void DesenfundarOn()
    {
        audioSource.PlayOneShot(sonidoDeDesenfundar);
    }

    public void CartuchoEntraOn()
    {
        audioSource.PlayOneShot(SonCartuEntra);
        RecargarMuniciones();
    }

    public void CartuchoSaleOn()
    {
        audioSource.PlayOneShot(SonCartuSale);
    }

    public void VacioOn()
    {
        audioSource.PlayOneShot(sonidoVacio);
        Invoke("ReiniciarRecargar", 0.1f);
    }

    void ReiniciarRecargar()
    {
        recargando = false;
    }
}

