using UnityEngine;
using UnityEngine.SceneManagement;

//Clase encargada de manejar el menú principal, permitiendo navegar entre escenas y mostrar/ocultar paneles de información
public class MenuManager : MonoBehaviour
{
    public GameObject panelComoJugar;
    public GameObject panelOpciones;
    public GameObject panelNiveles;

    public void ActivarPanelNiveles()
    {
        panelNiveles.SetActive(true);
    }

    public void Video()
    {
        SceneManager.LoadScene("Video");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void DesactivarPanelNiveles()
    {
        panelNiveles.SetActive(false);
    }

    public void Salir()
    {
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
