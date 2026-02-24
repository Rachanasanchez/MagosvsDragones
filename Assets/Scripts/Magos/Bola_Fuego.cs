using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que representa la bola de fuego lanzada por el mago de fuego. Esta bola se mueve a una velocidad constante y causa daño a los dragones al colisionar con ellos, destruyéndose después del impacto o después de un tiempo determinado.
public class Bola_Fuego : MonoBehaviour
{
    public float velocidad;
    public float dano;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.velocity = Vector2.right * velocidad;
        Destroy(gameObject, 15f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Dragones>(out var dragon))
        {
            dragon.TakeDamage(dano);
            Destroy(gameObject);

        }
    }
}
