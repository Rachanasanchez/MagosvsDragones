using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago_Laser : Magos
{
    public float distanciaAtaque;
    private Dragones target;

    private void Update()
    {
        target = LaneManager.Instance.ObtenerPrimerDragon(lane, transform.position.x);

        if (target == null || puedeAtacar == false)
        {
            GetComponent<Animator>().SetBool("Atacar", false);
            return;
        }

        float distanciaAlDragon = Vector3.Distance(transform.position, target.transform.position);

        if (distanciaAlDragon <= distanciaAtaque)
        {
            GetComponent<Animator>().SetBool("Atacar", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Atacar", false);
        }
    }


    void Atacar()
    {
        if (target != null)
        {
            SoundManager.Instance.PlayLimited(audioDisparo, 5, transform.position, 0.8f);
            target.TakeDamage(dano);
        }
    }

}
