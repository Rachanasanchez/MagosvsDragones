using System.Collections;
using UnityEngine;

public class DragonesSpawner : MonoBehaviour
{
    public GameObject[] dragonesPrefab;
    private Transform[] laneSpawnPoints;
    private int lineas;

    public int tiempoDeEspera;
    public int segundosOleada;
    public int cantidadDragones;
    public int cantidadFinal;



    private void Start()
    {
        lineas = Creacion_Casillas.Instancia.GetRows();

        laneSpawnPoints = new Transform[lineas];

        for (int i = 0; i < lineas; i++)
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

        StartCoroutine(OleadaDragones());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            int numeroLinea = Random.Range(0, lineas);
            SpawnDragon(numeroLinea);
        }
    }

    IEnumerator OleadaDragones()
    {
        float tiempoEntreSpawns = (float)segundosOleada / cantidadDragones;
        yield return new WaitForSeconds(tiempoDeEspera);

        //FASE NORMAL
        for (int i = 0; i < cantidadDragones; i++)
        {
            int numeroLinea = Random.Range(0, lineas);
            SpawnDragon(numeroLinea);
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }

        //OLEADA FINAL (todos seguidos)
        for (int i = 0; i < cantidadFinal; i++)
        {
            int numeroLinea = Random.Range(0, lineas);
            SpawnDragon(numeroLinea);
            yield return null; // para que se creen en frames distintos
        }

        Debug.Log("OLEADA FINAL COMPLETADA");
    }



    public void SpawnDragon(int numeroLinea)
    {
        int dragonElegido = Random.Range(0, dragonesPrefab.Length);

        GameObject dragon = Instantiate(dragonesPrefab[dragonElegido], laneSpawnPoints[numeroLinea].position, Quaternion.identity);
        dragon.GetComponent<Dragones>().SetLane(numeroLinea);
    }
}
