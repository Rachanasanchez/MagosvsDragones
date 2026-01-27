using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magos : MonoBehaviour
{
    public int lane;
    public Click_Casilla currentCell;

    public float vida = 5f;
    public bool puedeAtacar;
    public int dano;

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
