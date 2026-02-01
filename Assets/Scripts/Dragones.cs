using UnityEngine;

public class Dragones : MonoBehaviour
{
    public float speed;
    public float health;
    public float attack;
    public int lane;
    public AudioClip audioAtaque;
    public AudioClip audioVolar;
    public AudioSource audioSource;

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
