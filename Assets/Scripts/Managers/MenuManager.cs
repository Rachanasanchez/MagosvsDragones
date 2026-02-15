using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelComoJugar;

    public void Jugar()
    {
        SceneManager.LoadScene(1); 
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }

    public void MostrarComoJugar()
    {
        panelComoJugar.SetActive(true);
    }

    public void OcultarComoJugar()
    {
        panelComoJugar.SetActive(false);
    }

}
