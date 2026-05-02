using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicLevelDirector : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] dragonPrefabs;

    [Header("Lanes")]
    private Transform[] lanes;
    private int laneCount;

    [Header("Difficulty Base")]
    public float baseIntensityGrowth = 0.08f;
    public float maxIntensity = 999f;

    [Header("Spawn Settings")]
    public float spawnCooldownMin = 0.15f;
    public float spawnCooldownMax = 2.5f;

    public int baseMaxEnemiesOnField = 10;
    public int maxEnemiesHardCap = 80;

    [Header("Runtime")]
    private float currentIntensity;
    private float spawnTimer;
    private float elapsedTime;

    private float difficultyMultiplier = 1f;

    private bool isInfinite;
    private bool isRunning;

    private float levelDuration;
    private float levelTime;
    private bool finalWaveTriggered;

    [Header("Debug")]
    public bool debugMode = true;
    private float debugTimer;

    [Header("UI")]
    public Slider sliderRonda;
    private TMP_Text contadorText;
    private float remaining;
    private bool finished;

    private void Start()
    {
        SetupLanes();
    }

    private void Update()
    {
        if (finished) return;

        remaining -= Time.deltaTime;

        if (remaining <= 0f)
        {
            remaining = 0f;
            finished = true;

            contadorText.text = "OLEADA FINAL";
            return;
        }

        UpdateUI();
    }

    // =========================
    // INIT MODOS
    // =========================

    public void StartLevelMode(float duration, float multiplier)
    {
        StopAllCoroutines();

        isInfinite = false;
        difficultyMultiplier = multiplier;
        levelDuration = duration;

        GameObject sliderObj = GameObject.Find("Slider_Ronda");

        if (sliderObj != null)
        {
            sliderRonda = sliderObj.GetComponent<Slider>();
        }
        else
        {
            Debug.LogError("No se encontró el GameObject 'Slider_Ronda'");
        }

        GameObject obj = GameObject.Find("Contador");

        if (obj != null)
        {
            contadorText = obj.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogError("No se encontró un objeto llamado 'Contador' en la escena.");
        }

        remaining = duration;

        sliderRonda.maxValue = remaining;
        sliderRonda.value = remaining;

        finished = false;
        UpdateUI();

        ResetRuntime();

        StartCoroutine(LevelLoop());
    }

    void UpdateUI()
    {
        int totalSeconds = Mathf.CeilToInt(remaining);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        contadorText.text = $"{minutes:00}:{seconds:00}";

        sliderRonda.value = remaining;
    }

    public void StartInfiniteMode(float multiplier)
    {
        StopAllCoroutines();

        isInfinite = true;
        difficultyMultiplier = multiplier;

        ResetRuntime();

        StartCoroutine(InfiniteLoop());
    }

    void ResetRuntime()
    {
        elapsedTime = 0f;
        levelTime = 0f;
        currentIntensity = 0f;
        spawnTimer = 0f;
        isRunning = true;
    }

    // =========================
    // LANES
    // =========================

    void SetupLanes()
    {
        laneCount = Creacion_Casillas.Instancia.GetRows();
        lanes = new Transform[laneCount];

        for (int i = 0; i < laneCount; i++)
        {
            lanes[i] = GameObject.Find($"SpawnPoint_{i}")?.transform;
        }
    }

    // =========================
    // LEVEL MODE
    // =========================

    IEnumerator LevelLoop()
    {
        while (isRunning)
        {
            levelTime += Time.deltaTime;

            float timeProgress = levelTime / levelDuration;

            if (levelTime >= levelDuration)
            {
                isRunning = false;
                break;
            }

            RunDirectorLogic(timeProgress);

            yield return null;
        }

        yield return new WaitUntil(() =>
            GameObject.FindGameObjectsWithTag("Dragon").Length == 0
        );

        if (!finalWaveTriggered && levelTime >= levelDuration * 0.9f)
        {
            finalWaveTriggered = true;
            StartCoroutine(FinalWave());
        }
    }

    IEnumerator FinalWave()
    {
        Debug.Log("FINAL WAVE START");

        for (int i = 0; i < 25; i++)
        {
            SpawnDragon(1f);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Dragon").Length == 0);

        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform panel = canvas.Find("Ganar_Partida");

        panel.gameObject.SetActive(true);
    }

    // =========================
    // INFINITE MODE
    // =========================

    IEnumerator InfiniteLoop()
    {
        while (isRunning)
        {
            RunDirectorLogic(0f);
            yield return null;
        }
    }

    // =========================
    // CORE DIRECTOR
    // =========================

    void RunDirectorLogic(float timeProgress)
    {
        elapsedTime += Time.deltaTime;

        float timeFactor = Mathf.Pow(elapsedTime * baseIntensityGrowth, 1.3f);

        currentIntensity = Mathf.Clamp(timeFactor, 0, maxIntensity) * difficultyMultiplier;

        int enemyCount = GameObject.FindGameObjectsWithTag("Dragon").Length;

        float pressure = enemyCount / (float)GetDynamicEnemyCap();

        float spawnChance = CalculateSpawnChance(pressure);

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f && Random.value < spawnChance)
        {
            SpawnDragon(pressure);
            spawnTimer = GetDynamicCooldown(pressure);
        }

        if (debugMode)
        {
            debugTimer += Time.deltaTime;

            if (debugTimer >= 0.5f)
            {
                Debug.Log(
                    "TIME=" + elapsedTime.ToString("F1") +
                    " | INTENSITY=" + currentIntensity.ToString("F1") +
                    " | ENEMIES=" + enemyCount +
                    " | CAP=" + GetDynamicEnemyCap() +
                    " | PRESSURE=" + pressure.ToString("F2") +
                    " | SPAWN_TIMER=" + spawnTimer.ToString("F2")
                );

                debugTimer = 0f;
            }
        }
    }

    // =========================
    // SPAWN SYSTEM
    // =========================

    float CalculateSpawnChance(float pressure)
    {
        float intensityFactor = currentIntensity / maxIntensity;
        float lowPressureBonus = 1f - pressure;

        return Mathf.Clamp(
            0.2f +
            intensityFactor * 0.7f +
            lowPressureBonus * 0.3f,
            0.05f,
            0.98f
        );
    }

    float GetDynamicCooldown(float pressure)
    {
        float intensityFactor = currentIntensity / maxIntensity;

        float cooldown = Mathf.Lerp(
            spawnCooldownMax,
            spawnCooldownMin,
            intensityFactor
        );

        cooldown *= Mathf.Lerp(1.6f, 0.5f, 1f - pressure);

        return cooldown;
    }

    int GetDynamicEnemyCap()
    {
        float progression = Mathf.Clamp01(currentIntensity / maxIntensity);

        return Mathf.RoundToInt(
            Mathf.Lerp(baseMaxEnemiesOnField, maxEnemiesHardCap, progression)
        );
    }

    // =========================
    // SPAWN
    // =========================

    void SpawnDragon(float pressure)
    {
        int lane = GetWeightedLane();
        int prefab = Random.Range(0, dragonPrefabs.Length);

        GameObject dragon = Instantiate(
            dragonPrefabs[prefab],
            lanes[lane].position,
            Quaternion.identity
        );

        dragon.GetComponent<Dragones>().SetLane(lane);

        if (debugMode)
        {
            Debug.Log(
                "SPAWN | lane=" + lane +
                " | pressure=" + pressure.ToString("F2") +
                " | intensity=" + currentIntensity.ToString("F1")
            );
        }
    }

    int GetWeightedLane()
    {
        int[] laneWeight = new int[laneCount];

        foreach (GameObject d in GameObject.FindGameObjectsWithTag("Dragon"))
        {
            Dragones dr = d.GetComponent<Dragones>();
            if (dr != null)
                laneWeight[dr.lane]++;
        }

        int min = int.MaxValue;
        int bestLane = 0;

        for (int i = 0; i < laneCount; i++)
        {
            if (laneWeight[i] < min)
            {
                min = laneWeight[i];
                bestLane = i;
            }
        }

        return bestLane;
    }
}