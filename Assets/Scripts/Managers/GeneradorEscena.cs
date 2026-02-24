using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase encargada de generar una secuencia de objetos a partir de un array de prefabs, instanciándolos uno por uno y mostrando un mensaje en la consola cada vez que se crea uno, hasta que se hayan creado todos
public class GeneradorEscena : MonoBehaviour
{
    public GameObject[] prefabs;

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        while (currentIndex < prefabs.Length)
        {
            GameObject obj = Instantiate(prefabs[currentIndex], Vector3.zero, Quaternion.identity);

            yield return null;

            Debug.Log($"Se ha creado - {obj.name}");

            currentIndex++;
        }

        Debug.Log("Todos los objetos han terminado");
    }

}
