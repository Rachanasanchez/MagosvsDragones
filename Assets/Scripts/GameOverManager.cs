using UnityEngine;
using UnityEngine.SceneManagement;

// Este script se encarga de gestionar el panel de Game Over, mostrando el panel cuando el jugador pierde, y permitiendo reiniciar el nivel, avanzar al siguiente nivel o volver al menú principal.
// Es un singleton para que pueda ser accedido desde cualquier parte del juego sin necesidad de referencias directas.
public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject panelGameOver;
    private bool gameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dragon"))
        {
            Lose();
        }
    }


    public void Lose()
    {
        if (gameOver) return;
        gameOver = true;

        if (panelGameOver != null) panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
