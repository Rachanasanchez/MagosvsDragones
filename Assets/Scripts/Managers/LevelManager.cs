using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] botonesNiveles;
    public GameObject[] candados;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("NivelDesbloqueado"))
        {
            PlayerPrefs.SetInt("NivelDesbloqueado", 1);
        }

        ActualizarNiveles();
    }

    void ActualizarNiveles()
    {
        int nivelDesbloqueado = PlayerPrefs.GetInt("NivelDesbloqueado");

        for (int i = 0; i < botonesNiveles.Length; i++)
        {
            bool desbloqueado = i < nivelDesbloqueado;

            botonesNiveles[i].interactable = desbloqueado;

            if (candados[i] != null)
            {
                candados[i].SetActive(!desbloqueado);
            }
        }
    }

    public void CargarNivel(int nivel)
    {
        int nivelDesbloqueado = PlayerPrefs.GetInt("NivelDesbloqueado");

        if (nivel <= nivelDesbloqueado)
        {
            SceneManager.LoadScene("Nivel" + nivel);
        }
    }
}