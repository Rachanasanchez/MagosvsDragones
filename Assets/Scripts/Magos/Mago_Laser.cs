using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago_Laser : Magos
{
    private Dragones target;

    private void Update()
    {
        target = LaneManager.Instance.ObtenerPrimerDragon(lane);

        if (target == null || puedeAtacar == false)
        {
            gameObject.GetComponent<Animator>().SetBool("Atacar", false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Atacar", true);
        }
    }

    void Atacar()
    {
        if (target != null)
        {
            audioSource.PlayOneShot(audioDisparo);
        }
    }
}
