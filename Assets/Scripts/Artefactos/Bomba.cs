using UnityEngine;

public class Bomba : Unidades
{
    private bool haExplotado = false;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (haExplotado) return;

        Dragones dragon = other.GetComponent<Dragones>();

        if (dragon != null)
        {
            Explotar();
        }
    }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("La bomba detecto: " + other.name);

        if (haExplotado) return;

        Dragones dragon = other.GetComponent<Dragones>();

        if (dragon != null)
        {
            Debug.Log("La bomba explotó");
            Explotar();
        }
    }
    private void Explotar()
    {
        if (haExplotado) return;
        haExplotado = true;

        Dragones[] listaDragones = FindObjectsOfType<Dragones>();

        foreach (Dragones dragon in listaDragones)
        {
            if (dragon.lane == this.lane)
            {
                Destroy(dragon.gameObject);
            }
        }

        if (currentCell != null)
        {
            currentCell.Liberar();
            currentCell = null;
        }

        Destroy(gameObject);
    }
}