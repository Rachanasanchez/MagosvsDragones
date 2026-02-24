using UnityEngine;
using UnityEngine.SceneManagement;

// Este script se encarga de gestionar los datos del jugador, como la cantidad de cristales que tiene. Es un singleton para que pueda ser accedido desde cualquier parte del juego sin necesidad de referencias directas.

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    [Header("Sun Settings")]
    [SerializeField] private int cristalesIniciales = 0;

    public int cristalesActuales;

    public int CristalesActuales => cristalesActuales;

    private void Awake()
    {
        // Singleton seguro
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        cristalesActuales = cristalesIniciales;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cristalesActuales = 0;
    }

    public bool TieneSuficientesCristales(int amount)
    {
        return cristalesActuales >= amount;
    }

    public bool GastarCristales(int amount)
    {
        if (!TieneSuficientesCristales(amount))
            return false;

        cristalesActuales -= amount;
        //Debug.Log($"Soles gastados: {amount} | Restantes: {currentSun}");
        return true;
    }

    public void SumarCristales(int amount)
    {
        cristalesActuales += amount;
        //Debug.Log($"Soles añadidos: {amount} | Total: {currentSun}");
    }

}