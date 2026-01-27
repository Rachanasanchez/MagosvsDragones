using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago_Fuego : Magos
{
    private Dragones target;
    private void Update()
    {
        target = LaneManager.Instance.ObtenerPrimerDragon(lane);

        if(target == null || puedeAtacar == false)
        {
            gameObject.GetComponent<Animator>().SetBool("Atacar", false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Atacar", true);
        }
    }

    void Shoot()
    {
        if(target != null)
        {
            target.TakeDamage(dano);

        }
        // aquí luego instancias el proyectil
    }

}
