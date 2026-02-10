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


    [Header("Game Over")]
    public Transform finalTablero;
    private bool gameOverLanzado = false;

    void Update()
    {
        if (finalTablero == null) return;

        // Si los dragones van hacia la derecha
        if (!gameOverLanzado && transform.position.x >= finalTablero.position.x)
        {
            gameOverLanzado = true;
            GameOverManager.Instance?.Lose();
        }
    }

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
