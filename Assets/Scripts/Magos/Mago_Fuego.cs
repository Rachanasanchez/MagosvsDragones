using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que representa a un mago especializado en ataques a distancia con fuego.
public class Mago_Fuego : Magos
{
    public GameObject bolaFuego;
    private Dragones target;

    private void Update()
    {
        target = LaneManager.Instance.ObtenerPrimerDragon(lane, transform.position.x);

        if (target == null || puedeAtacar == false)
        {
            GetComponent<Animator>().SetBool("Atacar", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Atacar", true);
        }
    }


    void Shoot()
    {
        if(target != null)
        {
            SoundManager.Instance.PlayLimited(audioDisparo, 5, transform.position, 0.8f);
            Instantiate(bolaFuego, transform.position, transform.rotation);
        }
    }

}
