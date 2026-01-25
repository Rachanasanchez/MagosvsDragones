using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magos : MonoBehaviour
{
    public int lane;
    public Click_Casilla currentCell;

    public float health = 5f;
    public bool canAttack;

    public void Initialize(Click_Casilla cell)
    {
        currentCell = cell;
        lane = cell.Fila;
        canAttack = true;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
            Die();
    }

    protected void Die()
    {
        //currentCell.RemovePlant();
        Destroy(gameObject);
    }
}
