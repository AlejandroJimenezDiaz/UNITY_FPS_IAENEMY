using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI textoTMP;  // 🔹 Usa TextMeshProUGUI en vez de TextMesh
    public LogicaArma logicaArma;

    void Update()
    {
        textoTMP.text = $"{logicaArma.balasEnCartucho}/{logicaArma.tamañoDeCartucho}\n{logicaArma.balasRestantes}";
    }
}

