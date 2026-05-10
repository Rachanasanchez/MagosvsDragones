using System.Collections;
using UnityEngine;

public class DynamicLevelDirector : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] dragonPrefabs;

    [Header("Lanes")]
    private Transform[] lanes;
    private int laneCount;

    [Header("Intensity")]
    [Tooltip("Intensidad inicial")]
    public float intensity = 1f;

    [Tooltip("Velocidad de crecimiento exponencial")]
    public float intensityGrowth = 0.03f;

    [Header("Spawn")]
    [Tooltip("Cooldown inicial")]
    public float startSpawnCooldown = 1.5f;

    [Tooltip("Cooldown mínimo absoluto")]
    public float minimumSpawnCooldown = 0.02f;

    [Header("Enemy Cap")]
    [Tooltip("Máximo inicial de enemigos")]
    public int startEnemyCap = 5;

    [Tooltip("Cuánto aumenta el cap con la intensidad")]
    public float enemyCapGrowth = 1.5f;

    [Header("Debug")]
    public bool debugMode = true;

    private float elapsedTime;
    private float spawnTimer;

    private bool running;

    private float debugTimer;

    // =========================
    // START
    // =========================

    void Start()
    {
        SetupLanes();

        StartInfiniteMode();
    }

    void Update()
    {
        // DEBUG SPEED
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 3f;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 1f;
        }
    }

    // =========================
    // INIT
    // =========================

    public void StartInfiniteMode()
    {
        StopAllCoroutines();

        elapsedTime = 0f;
        spawnTimer = 0f;

        running = true;

        StartCoroutine(InfiniteLoop());
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
            GameObject obj = GameObject.Find($"SpawnPoint_{i}");

            if (obj != null)
            {
                lanes[i] = obj.transform;
            }
            else
            {
                Debug.LogError($"No existe SpawnPoint_{i}");
            }
        }
    }

    // =========================
    // MAIN LOOP
    // =========================

    IEnumerator InfiniteLoop()
    {
        while (running)
        {
            RunDirector();

            yield return null;
        }
    }

    // =========================
    // DIRECTOR
    // =========================

    void RunDirector()
    {
        elapsedTime += Time.deltaTime;

        // CRECIMIENTO EXPONENCIAL
        intensity = Mathf.Exp(elapsedTime * intensityGrowth);

        int enemyCount = GameObject.FindGameObjectsWithTag("Dragon").Length;

        int enemyCap = GetEnemyCap();

        spawnTimer -= Time.deltaTime;

        // SPAWN
        if (spawnTimer <= 0f)
        {
            if (enemyCount < enemyCap)
            {
                SpawnDragon();

                spawnTimer = GetSpawnCooldown();
            }
        }

        // DEBUG
        if (debugMode)
        {
            debugTimer += Time.deltaTime;

            if (debugTimer >= 0.5f)
            {
                Debug.Log(
                    "TIME=" + elapsedTime.ToString("F1") +
                    " | INTENSITY=" + intensity.ToString("F2") +
                    " | ENEMIES=" + enemyCount +
                    " | CAP=" + enemyCap +
                    " | COOLDOWN=" + spawnTimer.ToString("F2")
                );

                debugTimer = 0f;
            }
        }
    }

    // =========================
    // SPAWN COOLDOWN
    // =========================

    float GetSpawnCooldown()
    {
        // Cuanta más intensidad,
        // más rápido spawnea

        float cooldown =
            startSpawnCooldown / intensity;

        return Mathf.Max(
            minimumSpawnCooldown,
            cooldown
        );
    }

    // =========================
    // ENEMY CAP
    // =========================

    int GetEnemyCap()
    {
        return Mathf.RoundToInt(
            startEnemyCap +
            (intensity * enemyCapGrowth)
        );
    }

    // =========================
    // SPAWN
    // =========================

    void SpawnDragon()
    {
        if (dragonPrefabs.Length <= 0)
            return;

        int lane = GetRandomBalancedLane();

        int prefabIndex = Random.Range(
            0,
            dragonPrefabs.Length
        );

        GameObject dragon = Instantiate(
            dragonPrefabs[prefabIndex],
            lanes[lane].position,
            Quaternion.identity
        );

        Dragones dr = dragon.GetComponent<Dragones>();

        if (dr != null)
        {
            dr.SetLane(lane);
        }

        if (debugMode)
        {
            Debug.Log(
                "SPAWN -> Lane=" + lane
            );
        }
    }

    // =========================
    // RANDOM BALANCED LANE
    // =========================

    int GetRandomBalancedLane()
    {
        int[] laneWeights = new int[laneCount];

        // Contar enemigos por lane
        foreach (GameObject dragon in GameObject.FindGameObjectsWithTag("Dragon"))
        {
            Dragones dr = dragon.GetComponent<Dragones>();

            if (dr != null)
            {
                if (dr.lane >= 0 && dr.lane < laneCount)
                {
                    laneWeights[dr.lane]++;
                }
            }
        }

        // Buscar menor cantidad
        int minWeight = int.MaxValue;

        for (int i = 0; i < laneCount; i++)
        {
            if (laneWeights[i] < minWeight)
            {
                minWeight = laneWeights[i];
            }
        }

        // Guardar lanes válidas
        System.Collections.Generic.List<int> validLanes =
            new System.Collections.Generic.List<int>();

        for (int i = 0; i < laneCount; i++)
        {
            // Permite aleatoriedad manteniendo balance
            if (laneWeights[i] <= minWeight + 1)
            {
                validLanes.Add(i);
            }
        }

        // Random real
        return validLanes[
            Random.Range(0, validLanes.Count)
        ];
    }
}