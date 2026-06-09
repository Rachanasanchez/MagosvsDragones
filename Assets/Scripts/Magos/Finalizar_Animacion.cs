using UnityEngine;

public class Finalizar_Animacion : MonoBehaviour
{
    public AudioClip Sonido_explosion;

    public void Eliminar_Objeto()
    {
        Destroy(gameObject);
    }

    public void Sonido_Bomba()
    {
        if (Sonido_explosion != null)
            SoundManager.Instance.PlayLimited(Sonido_explosion, 5, transform.position, 0.8f);
    }
}
