using UnityEngine;

public class Unidad_Mago_Fuego : Unidades
{
    public GameObject bolaFuego;
    public AudioClip audioDisparo;

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
            Instantiate(bolaFuego, transform.position, transform.rotation);
        }
    }
}
