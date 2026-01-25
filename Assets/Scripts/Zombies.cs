using UnityEngine;

public class Zombies : MonoBehaviour
{
    public float speed = 1f;
    public float health = 5f;
    public float attack = 5f;
    public int lane;

    public void SetLane(int l)
    {
        lane = l;
        LaneManager.Instance.RegisterZombie(this, lane);
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
        LaneManager.Instance.RemoveZombie(this, lane);
    }

}
