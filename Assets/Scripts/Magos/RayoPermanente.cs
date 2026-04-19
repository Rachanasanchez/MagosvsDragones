using System.Collections;
using UnityEngine;

public class RayoPermanente : MonoBehaviour
{
    public int segundosCasillaBloqueada;

    private Click_Casilla casillaBloqueada;
    private SpriteRenderer sr;

    public void Inicializar(Click_Casilla casilla)
    {
        casillaBloqueada = casilla;

        if (casillaBloqueada != null)
        {
            casillaBloqueada.isAvailable = false;
        }
    }

    private void Start()
    {
        StartCoroutine(EsperarYDesbloquear());
        sr = GetComponent<SpriteRenderer>();
    }

    private IEnumerator EsperarYDesbloquear()
    {
        yield return new WaitForSeconds(segundosCasillaBloqueada);

        if (casillaBloqueada != null)
        {
            casillaBloqueada.isAvailable = true;
        }

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