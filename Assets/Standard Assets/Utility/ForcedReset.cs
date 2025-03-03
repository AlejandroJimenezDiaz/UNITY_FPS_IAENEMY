using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Para usar UI en caso necesario

public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // Si se presiona el botón de reinicio
        if (Input.GetButtonDown("ResetObject")) // También puedes usar Input.GetKeyDown(KeyCode.R)
        {
            // Reinicia la escena activa en lugar de usar GetSceneAt(0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
