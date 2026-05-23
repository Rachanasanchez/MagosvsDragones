using System.Collections;
using UnityEngine;

public class DynamicLevelDirector : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] dragonPrefabs;

    [Header("Lanes")]
    private Transform[] lanes;
    private int laneCount;

    [Header("Difficulty Growth")]
    public float intensityGrowthSpeed = 0.02f;

    [Header("Enemy Scaling")]
    public int startEnemyTarget = 3;
    public int maxEnemyTarget = 150;
    public float enemyGrowthPower = 1.4f;

    [Header("Spawn Control")]
    public float spawnBurstDelay = 0.05f; // anti freeze

    [Header("Debug")]
    public bool debugMode = true;
    private float debugTimer;

    private float elapsedTime;
    private float spawnTimer;

    private void Start()
    {
        SetupLanes();
        StartCoroutine(InfiniteLoop());
    }

    void SetupLanes()
    {
        laneCount = Creacion_Casillas.Instancia.GetRows();
        lanes = new Transform[laneCount];

        for (int i = 0; i < laneCount; i++)
        {
            lanes[i] = GameObject.Find($"SpawnPoint_{i}")?.transform;
        }
    }

    IEnumerator InfiniteLoop()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime;

            float intensity = GetIntensity();

            int enemyCount = GameObject.FindGameObjectsWithTag("Dragon").Length;

            int targetEnemies = GetTargetEnemies(intensity);

            // 🧠 DIFERENCIA REAL
            int deficit = targetEnemies - enemyCount;

            // 🔥 SPAM CONTROLADO (evita spawnear todo en 1 frame)
            if (deficit > 0)
            {
                spawnTimer -= Time.deltaTime;

                if (spawnTimer <= 0f)
                {
                    SpawnBurst(deficit, intensity);
                    spawnTimer = spawnBurstDelay;
                }
            }
            else
            {
                spawnTimer = 0f;
            }

            if (debugMode)
            {
                debugTimer += Time.deltaTime;

                if (debugTimer >= 0.5f)
                {
                    Debug.Log(
                        $"TIME={elapsedTime:F1} | INT={intensity:F2} | ENEMIES={enemyCount} | TARGET={targetEnemies} | DEFICIT={deficit}"
                    );

                    debugTimer = 0f;
                }
            }

            yield return null;
        }
    }

    // ----------------------------
    // INTENSIDAD CONTINUA
    // ----------------------------
    float GetIntensity()
    {
        float t = elapsedTime * intensityGrowthSpeed;
        return Mathf.Clamp01(1f - Mathf.Exp(-t));
    }

    // ----------------------------
    // TARGET REAL
    // ----------------------------
    int GetTargetEnemies(float intensity)
    {
        float growth = Mathf.Pow(intensity, enemyGrowthPower);
        return Mathf.RoundToInt(Mathf.Lerp(startEnemyTarget, maxEnemyTarget, growth));
    }

    // ----------------------------
    // SPAWN DIRECTO PARA RELLENAR
    // ----------------------------
    void SpawnBurst(int amount, float intensity)
    {
        int spawns = Mathf.Min(amount, 5); // 🔥 límite por frame (importante)

        for (int i = 0; i < spawns; i++)
        {
            int lane = GetWeightedLane();
            int prefab = Random.Range(0, dragonPrefabs.Length);

            GameObject dragon = Instantiate(
                dragonPrefabs[prefab],
                lanes[lane].position,
                Quaternion.identity
            );

            dragon.GetComponent<Dragones>().SetLane(lane);
        }
    }

    // ----------------------------
    // LANES BALANCEADOS
    // ----------------------------
    int GetWeightedLane()
    {
        int[] laneWeight = new int[laneCount];

        GameObject[] dragons = GameObject.FindGameObjectsWithTag("Dragon");

        foreach (GameObject d in dragons)
        {
            Dragones dr = d.GetComponent<Dragones>();
            if (dr != null)
                laneWeight[dr.lane]++;
        }

        int bestLane = 0;
        int min = int.MaxValue;

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