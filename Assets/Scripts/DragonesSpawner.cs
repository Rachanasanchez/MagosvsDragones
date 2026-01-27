using UnityEngine;

public class DragonesSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    private Transform[] laneSpawnPoints; // uno por fila

    public int numerro;

    private void Start()
    {
        //int lanes = creacion_casillas.rows;

        int lanes = 5;
        laneSpawnPoints = new Transform[lanes];

        for (int i = 0; i < lanes; i++)
        {
            GameObject sp = GameObject.Find($"SpawnPoint_{i}");

            if (sp != null)
            {
                laneSpawnPoints[i] = sp.transform;
            }
            else
            {
                Debug.LogError($"No se encontró SpawnPoint_{i}");
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {           

            SpawnZombie(numerro);
        }
    }

    public void SpawnZombie(int lane)
    {
        GameObject z = Instantiate(zombiePrefab, laneSpawnPoints[lane].position, Quaternion.identity);
        z.GetComponent<Dragones>().SetLane(lane);
    }
}
