using UnityEngine;

//Clase encargada de manejar la pausa del juego, deteniendo el tiempo y mostrando un menú de pausa
public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject menuPausa;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            menuPausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            menuPausa.SetActive(false);
        }
    }
}
