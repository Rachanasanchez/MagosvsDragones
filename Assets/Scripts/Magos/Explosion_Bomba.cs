using System.Collections;
using UnityEngine;

public class Explosion_Bomba : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeYDestruir());
    }

    private IEnumerator FadeYDestruir()
    {
        // Espera inicial
        yield return new WaitForSeconds(0.5f);

        float duracionFade = 0.5f;
        float tiempo = 0f;

        Color colorInicial = sr.color;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, tiempo / duracionFade);
            sr.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }
}