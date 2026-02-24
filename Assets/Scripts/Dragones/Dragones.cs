using UnityEngine;

//Clase padre de los dragones, con atributos comunes y métodos básicos como recibir daño y morir. También se encarga de registrarse en el LaneManager para que este pueda llevar un control de qué dragones hay en cada línea.

public class Dragones : MonoBehaviour
{
    public float speed;
    public float health;
    public float attack;
    public int lane;
    public AudioClip audioAtaque;
    public AudioClip audioVolar;

    public void SetLane(int l)
    {
        lane = l;
        LaneManager.Instance.RegistrarDragon(this, lane);
    }
   
    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LaneManager.Instance.QuitarDragon(this, lane);
    }

}
