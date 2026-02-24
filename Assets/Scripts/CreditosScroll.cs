using UnityEngine;

// Esta clase se encarga de hacer que el texto de los créditos suba lentamente
public class CreditosScroll : MonoBehaviour
{
    public float velocidad = 30f;

    void Update()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
    }
}
