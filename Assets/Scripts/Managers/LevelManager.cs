using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] botonesNiveles;
    public GameObject[] candados;

    private void Start()
    {
        ActualizarNiveles();
    }

    void ActualizarNiveles()
    {
        for (int i = 0; i < botonesNiveles.Length; i++)
        {
            int levelNumber = i + 1;

            bool desbloqueado = SistemaGuardado.IsLevelUnlocked(levelNumber);

            botonesNiveles[i].interactable = desbloqueado;

            if (candados[i] != null)
            {
                candados[i].SetActive(!desbloqueado);
            }
        }
    }

    public void CargarNivel(int nivel)
    {
        if (SistemaGuardado.IsLevelUnlocked(nivel))
        {
            SceneManager.LoadScene("Nivel" + nivel);
        }
    }
}