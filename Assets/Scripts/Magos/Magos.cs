using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que representa a cada mago en el juego, con sus atributos como vida, daño y la celda en la que se encuentra. También incluye métodos para inicializar al mago, recibir daño y morir, liberando la celda que ocupaba.
public class Magos : MonoBehaviour
{
    public int lane;
    public Click_Casilla currentCell;

    public float vida;
    public bool puedeAtacar;
    public int dano;
    public AudioClip audioDisparo;

    public void Initialize(Click_Casilla cell)
    {
        currentCell = cell;
        lane = cell.Fila;
        puedeAtacar = true;
    }

    public void TakeDamage(float dmg)
    {
        vida -= dmg;

        if (vida <= 0)
            Die();
    }

    protected void Die()
    {
        if (currentCell != null)
        {
            currentCell.Liberar();   
            currentCell = null;
        }

        Destroy(gameObject);
    }
}
