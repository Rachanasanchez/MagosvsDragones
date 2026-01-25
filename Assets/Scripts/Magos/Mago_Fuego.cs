using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago_Fuego : Magos
{
    public float shootCooldown = 1f;
    public int damage;
    private Zombies target;
    private void Update()
    {
        target = LaneManager.Instance.GetFirstZombie(lane);

        if(target == null || canAttack == false)
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
            target.TakeDamage(damage);

        }
        // aquí luego instancias el proyectil
    }

}
