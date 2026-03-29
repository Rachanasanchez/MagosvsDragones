using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hechizo : MonoBehaviour
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
