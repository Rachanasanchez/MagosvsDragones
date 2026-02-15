using UnityEngine;

public class CreditosScroll : MonoBehaviour
{
    public float velocidad = 30f;

    void Update()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
    }
}
