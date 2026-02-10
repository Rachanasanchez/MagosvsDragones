using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (panelGameOver != null) panelGameOver.SetActive(false);
        Time.timeScale = 1f;
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

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
