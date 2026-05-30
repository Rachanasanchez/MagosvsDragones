using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Este script se encarga de gestionar el panel de Game Over, mostrando el panel cuando el jugador pierde, y permitiendo reiniciar el nivel, avanzar al siguiente nivel o volver al menú principal.
// Es un singleton para que pueda ser accedido desde cualquier parte del juego sin necesidad de referencias directas.
public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public GameObject panelGameOver;
    public float tiempoSupervivencia;
    public TMP_Text contadorNivel5Text;
    public TMP_Text textoFinal;
    private string sceneName;

    private bool gameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        sceneName = SceneManager.GetActiveScene().name;

    }

    void Update()
    {
        if (sceneName == "Nivel5")
        {
            tiempoSupervivencia += Time.deltaTime;

            int minutos = Mathf.FloorToInt(tiempoSupervivencia / 60);
            int segundos = Mathf.FloorToInt(tiempoSupervivencia % 60);

            contadorNivel5Text.text = $"{minutos:00}:{segundos:00}";
        }
        
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


        if (sceneName == "Nivel5")
        {
            string tiempoString = FormatearTiempo(tiempoSupervivencia);

            bool nuevoRecord = SistemaGuardado.EsNuevoRecord(tiempoString);

            if (nuevoRecord)
                SistemaGuardado.GuardarTiempoNivelInfinito(tiempoString);

            string recordActual = PlayerPrefs.GetString("NivelInfinito", "00:00");

            textoFinal.text = nuevoRecord
                ? $"¡NUEVO RECORD!\nHas aguantado {tiempoString}"
                : $"Has aguantado {tiempoString}\nTu record es de: {recordActual}";
        }

        if (panelGameOver != null) panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    private string FormatearTiempo(float segundos)
    {
        int minutos = Mathf.FloorToInt(segundos / 60);
        int segundosRestantes = Mathf.FloorToInt(segundos % 60);

        return minutos.ToString("00") + ":" + segundosRestantes.ToString("00");
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
