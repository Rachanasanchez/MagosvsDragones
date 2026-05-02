using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Clase encargada de generar las oleadas de dragones. Tiene un contador que muestra el tiempo restante para la siguiente oleada, y al finalizar la oleada final muestra un panel de victoria.

public class DragonesSpawner : MonoBehaviour
{
    public GameObject[] dragonesPrefab;
    private Transform[] laneSpawnPoints;
    private int lineas;

    public int tiempoDeEspera;
    public int segundosOleada;
    public int cantidadDragones;
    public int cantidadFinal;
    public Slider sliderRonda;


    private TMP_Text contadorText;
    private float remaining;
    private bool finished;


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

        GameObject sliderObj = GameObject.Find("Slider_Ronda");

        if (sliderObj != null)
        {
            sliderRonda = sliderObj.GetComponent<Slider>();
        }
        else
        {
            Debug.LogError("No se encontró el GameObject 'Slider_Ronda'");
        }

        StartCoroutine(OleadaDragones());

        GameObject obj = GameObject.Find("Contador");

        if (obj != null)
        {
            contadorText = obj.GetComponent<TMP_Text>();
        }
        else
        {
            Debug.LogError("No se encontró un objeto llamado 'Contador' en la escena.");
        }

        remaining = segundosOleada + tiempoDeEspera;

        sliderRonda.maxValue = remaining;
        sliderRonda.value = remaining;

        finished = false;
        UpdateUI();


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
    void UpdateUI()
    {
        int totalSeconds = Mathf.CeilToInt(remaining);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        contadorText.text = $"{minutes:00}:{seconds:00}";

        sliderRonda.value = remaining;
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
            yield return new WaitForSeconds(0.2f);
        }


        //Esperar hasta que no quede ningún dragón
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Dragon").Length == 0);

        Transform canvas = FindObjectOfType<Canvas>().transform;
        Transform panel = canvas.Find("Ganar_Partida");

        panel.gameObject.SetActive(true);

    }



    public void SpawnDragon(int numeroLinea)
    {
        int dragonElegido = Random.Range(0, dragonesPrefab.Length);

        GameObject dragon = Instantiate(dragonesPrefab[dragonElegido], laneSpawnPoints[numeroLinea].position, Quaternion.identity);
        dragon.GetComponent<Dragones>().SetLane(numeroLinea);
    }
}
