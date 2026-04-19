using System.Collections;
using System.Linq;
using UnityEngine;

public class Unidad_Bomba : Unidades
{
    public GameObject fuegoBombaPrefab;
    private bool haExplotado = false;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public override void TakeDamage(float dmg)
    {
        vida -= dmg;

        if (haExplotado) return;

        Explotar();
    }

    private void Explotar()
    {
        if (haExplotado) return;
        StartCoroutine(ExplotarCoroutine());
    }

    private IEnumerator ExplotarCoroutine()
    {
        haExplotado = true;
        sr.enabled = false;

        Dragones[] listaDragones = FindObjectsOfType<Dragones>();

        foreach (Dragones dragon in listaDragones)
        {
            if (dragon.lane == this.lane)
            {
                dragon.TakeDamage(dano);
            }
        }

        // Solo casillas de la misma fila
        Click_Casilla[] casillas = FindObjectsOfType<Click_Casilla>()
            .Where(c => c.Fila == this.lane)
            .OrderBy(c => c.Columna)
            .ToArray();

        int posicionBomba = currentCell.Columna;
        int max = casillas.Length - 1;

        Instantiate(fuegoBombaPrefab, casillas[posicionBomba].transform.position, Quaternion.identity);

        for (int distancia = 1; distancia < casillas.Length; distancia++)
        {
            yield return new WaitForSeconds(0.05f);

            int izquierda = posicionBomba - distancia;
            int derecha = posicionBomba + distancia;

            if (izquierda >= 0)
            {
                Instantiate(fuegoBombaPrefab, casillas[izquierda].transform.position, Quaternion.identity);
            }

            if (derecha <= max)
            {
                Instantiate(fuegoBombaPrefab, casillas[derecha].transform.position, Quaternion.identity);
            }
        }

        Die();
    }

}