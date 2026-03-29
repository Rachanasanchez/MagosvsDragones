using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase que representa a un mago especializado en ataques cercanos con su espada.
//Este mago busca al primer dragón en su carril y ataca si está dentro de su rango de ataque, reproduciendo un sonido de disparo y aplicando daño al dragón objetivo.

public class Mago_Rayo : Magos
{
    public GameObject hechizo;
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
        if (target != null)
        {
            SoundManager.Instance.PlayLimited(audioDisparo, 5, transform.position, 0.8f);
            Instantiate(hechizo, transform.position, transform.rotation);
        }
    }

}
