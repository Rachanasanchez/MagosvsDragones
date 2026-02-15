using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    [Header("Sun Settings")]
    [SerializeField] private int startingSun = 0;

    public int currentSun;

    public int CurrentSun => currentSun;

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

        currentSun = startingSun;
    }

    // ------------------------
    // SUNS
    // ------------------------

    private void OnEnable()
    {
        currentSun = 0;
    }

    public bool HasEnoughSun(int amount)
    {
        return currentSun >= amount;
    }

    public bool SpendSun(int amount)
    {
        if (!HasEnoughSun(amount))
            return false;

        currentSun -= amount;
        //Debug.Log($"Soles gastados: {amount} | Restantes: {currentSun}");
        return true;
    }

    public void AddSun(int amount)
    {
        currentSun += amount;
        //Debug.Log($"Soles añadidos: {amount} | Total: {currentSun}");
    }

    public void SetSun(int amount)
    {
        currentSun = Mathf.Max(0, amount);
        //Debug.Log($"Soles fijados a: {currentSun}");
    }
}