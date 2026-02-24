using UnityEngine;

//Clase del dragón que sopla, empujando los cristales hacia atrás. Tiene un área de efecto circular, y se puede configurar para que solo afecte a objetos de una capa específica o a objetos con un tag específico.
//El empuje se compone de una fuerza hacia atrás y una fuerza lateral hacia arriba o hacia abajo dependiendo de la posición del cristal respecto al dragón.

public class Dragon_Soplar : Dragones
{

    [Header("Soplar (cristales)")]
    public float pushForce = 12f;                 
    public float sideForce = 6f;                  
    public float affectRadius = 1.5f;            
    public LayerMask crystalLayer;                
    public string crystalTag = "Cristal";         
    public bool useLayerOnly = true;              

    private void Update()
    {
        WalkForward();        
    }

    private void WalkForward()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void Soplar()
    {
        Collider2D[] hits;

        if (useLayerOnly)
        {
            hits = Physics2D.OverlapCircleAll(transform.position, affectRadius, crystalLayer);
        }
        else
        {
            hits = Physics2D.OverlapCircleAll(transform.position, affectRadius);
        }

        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D c = hits[i];

            if (!useLayerOnly && !c.CompareTag(crystalTag))
                continue;

            Rigidbody2D rb = c.attachedRigidbody;
            if (rb == null) continue;

            // Arriba / abajo respecto al dragón (en Y)
            float side = (c.transform.position.y >= transform.position.y) ? 1f : -1f;

            Vector2 forceDir =
                (Vector2.left * pushForce) +
                (Vector2.up * sideForce * side);

            rb.AddForce(forceDir, ForceMode2D.Impulse);
        }

        if (audioAtaque != null)
            SoundManager.Instance.PlayLimited(audioAtaque, 5, transform.position, 0.8f);

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, affectRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.left + Vector3.up).normalized * 2f);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.left - Vector3.up).normalized * 2f);
    }



}
