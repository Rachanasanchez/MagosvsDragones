using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelComoJugar;
    public GameObject panelOpciones;

    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1"); 
    }

    public void Video()
    {
        SceneManager.LoadScene("Video");
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

    public void MostrarOpciones()
    {
        panelOpciones.SetActive(true);
    }

    public void OcultarOpciones()
    {
        panelOpciones.SetActive(false);
    }

}
