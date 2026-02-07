using UnityEngine;
using System.Collections;

public class GeneradorCristales : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject prefabCristal;

    [Header("Spawn")]
    [SerializeField] private float tiempoEntreCristales = 3f;
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float alturaSpawn = 5f;

    [Header("Desaparición")]
    [SerializeField] private float segundosParaDesaparecerEnSuelo = 6f;

    [Header("Imagenes")]
    [SerializeField] private Sprite[] listaSprites;


    private void Start()
    {
        InvokeRepeating(nameof(GenerarCristal), 1f, tiempoEntreCristales);
    }

    private void GenerarCristal()
    {
        if (prefabCristal == null)
        {
            Debug.LogError("GeneradorCristales: falta asignar prefabCristal.");
            return;
        }


        Vector3 pos = new Vector3(Random.Range(minX, maxX), alturaSpawn, 0f);
        GameObject cristal = Instantiate(prefabCristal, pos, Quaternion.identity);

        SpriteRenderer sr = cristal.GetComponent<SpriteRenderer>();
        sr.sprite = listaSprites[Random.Range(0, listaSprites.Length)];

        // Le añadimos el comportamiento de desaparecer al tocar el suelo
        var auto = cristal.GetComponent<CristalAutoDestruir>();
        if (auto == null) auto = cristal.AddComponent<CristalAutoDestruir>();
        auto.Configurar(segundosParaDesaparecerEnSuelo);
    }
}

// ?? ESTA CLASE ESTÁ EN EL MISMO ARCHIVO (no creas otro .cs)
public class CristalAutoDestruir : MonoBehaviour
{
    private float segundos;
    private bool yaContando = false;

    public void Configurar(float segundosParaDestruir)
    {
        segundos = segundosParaDestruir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (yaContando) return;

        if (collision.gameObject.CompareTag("Suelo"))
        {
            yaContando = true;
            StartCoroutine(DestruirTrasTiempo());
        }
    }

    private IEnumerator DestruirTrasTiempo()
    {
        yield return new WaitForSeconds(segundos);
        Destroy(gameObject);
    }
}
