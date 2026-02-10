using UnityEngine;

public class Dragon_Soplar : Dragones
{
    private Click_Casilla targetCell;

    [Header("Distancia para atacar")]
    public float attackRange = 1.5f;

    [Header("Soplar (cristales)")]
    public float pushForce = 12f;                 // fuerza del soplido
    public float sideForce = 6f;                  // fuerza lateral (para que se vayan a los lados)
    public float affectRadius = 1.5f;             // radio del soplido alrededor de la casilla objetivo
    public LayerMask crystalLayer;                // layer donde están los cristales
    public string crystalTag = "Cristal";         // por si prefieres tag
    public bool useLayerOnly = true;              // si true usa LayerMask, si false usa tag

    private void Update()
    {
        // Buscar objetivo si no hay objetivo
        if (targetCell == null || !targetCell.isOccupied)
        {
            targetCell = GetNextCellWithPlant();
        }

        if (targetCell != null && targetCell.isOccupied)
        {
            AttackBehavior();
        }
        else
        {
            WalkForward();
        }
    }

    private void AttackBehavior()
    {
        Vector3 targetPos = targetCell.transform.position;
        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance > attackRange)
        {
            // Caminar hacia el objetivo
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetPos.x, transform.position.y, transform.position.z),
                speed * Time.deltaTime
            );

            GetComponent<Animator>().SetBool("Atacar", false);
        }
        else
        {
            // En rango -> atacar
            GetComponent<Animator>().SetBool("Atacar", true);
        }
    }

    private void WalkForward()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        GetComponent<Animator>().SetBool("Atacar", false);
    }

    // Llamar desde el evento de animación (igual que Hacer_Dano en el eléctrico)
    public void Soplar()
    {
        // Si no hay casilla objetivo, no sopla
        if (targetCell == null) return;

        Vector3 center = targetCell.transform.position;

        Collider[] hits;
        if (useLayerOnly)
        {
            hits = Physics.OverlapSphere(center, affectRadius, crystalLayer);
        }
        else
        {
            hits = Physics.OverlapSphere(center, affectRadius);
        }

        for (int i = 0; i < hits.Length; i++)
        {
            Collider c = hits[i];

            // Si usas tag, filtramos aquí
            if (!useLayerOnly && !c.CompareTag(crystalTag))
                continue;

            Rigidbody rb = c.attachedRigidbody;
            if (rb == null) continue;

            // Direcciones: empuje principal hacia "la izquierda" (como si los sacara del camino),
            // y componente lateral dependiendo de si está arriba/abajo del dragón (en Z)
            float side = (c.transform.position.z >= transform.position.z) ? 1f : -1f;

            Vector3 forceDir =
                (Vector3.left * pushForce) +
                (Vector3.forward * sideForce * side);

            rb.AddForce(forceDir, ForceMode.Impulse);
        }

        // sonido de soplar si quieres reutilizar audios
        if (audioSource != null && audioAtaque != null)
            audioSource.PlayOneShot(audioAtaque);

        // Importante: NO hacemos daño al mago
    }

    public void sonidoVolar()
    {
        if (audioSource != null && audioVolar != null)
            audioSource.PlayOneShot(audioVolar);
    }

    // Igual que en tu script: busca la primera casilla ocupada por delante del dragón
    private Click_Casilla GetNextCellWithPlant()
    {
        int columns = GridManager.Instance.grid.GetLength(1);

        for (int col = 0; col < columns; col++)
        {
            Click_Casilla cell = GridManager.Instance.grid[lane, col];

            if (cell != null && cell.isOccupied)
            {
                if (cell.transform.position.x < transform.position.x)
                    return cell;
            }
        }
        return null;
    }
}
