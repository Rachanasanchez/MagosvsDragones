using UnityEngine;

public class Dragon_Doble : Dragones
{
    public GameObject rayoDoblePrefab;
    private Click_Casilla targetCell;

    [Header("Distancia para atacar")]
    public float attackRange;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            targetCell = GetNextCellWithMage();
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
        if (targetCell == null) return;

        Vector3 targetPos = targetCell.transform.position;
        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetPos.x, transform.position.y, transform.position.z),
                speed * Time.deltaTime
            );

            anim.SetBool("Atacar", false);
        }
        else
        {
            if (!isAttacking)
            {
                isAttacking = true;
                lockedTarget = targetCell;
                anim.SetBool("Atacar", true);
            }
        }
    }

    private void WalkForward()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        anim.SetBool("Atacar", false);
    }

    public void Hacer_Dano()
    {
        if (lockedTarget == null || !lockedTarget.isOccupied) return;
        if (targetCell == null || !targetCell.isOccupied) return;

        Unidades unidadObjetivo = targetCell.magoEnCasilla;

        if (unidadObjetivo == null)
        {
            targetCell.Liberar();
            targetCell = null;
            lockedTarget = null;
            return;
        }

        AtacarCasilla(targetCell);

        AtacarCasilla(GetCasilla(targetCell.Fila - 1, targetCell.Columna));
        AtacarCasilla(GetCasilla(targetCell.Fila + 1, targetCell.Columna));

        SoundManager.Instance.PlayLimited(audioAtaque, 5, transform.position, 0.8f);

        if (!targetCell.isOccupied)
            targetCell = null;
    }

    private Click_Casilla GetCasilla(int fila, int columna)
    {
        int filas = GridManager.Instance.grid.GetLength(0);
        int columnas = GridManager.Instance.grid.GetLength(1);

        if (fila < 0 || fila >= filas) return null;
        if (columna < 0 || columna >= columnas) return null;

        return GridManager.Instance.grid[fila, columna];
    }

    private void AtacarCasilla(Click_Casilla casilla)
    {
        if (casilla == null || !casilla.isOccupied) return;

        Unidades unidad = casilla.magoEnCasilla;
        if (unidad == null) return;

        Instantiate(rayoDoblePrefab, casilla.transform.position, Quaternion.identity);

        unidad.TakeDamage(attack);
    }

    public void FinAtaque()
    {
        isAttacking = false;
        lockedTarget = null;
        anim.SetBool("Atacar", false);
    }

    public void sonidoVolar()
    {
        SoundManager.Instance.PlayLimited(audioVolar, 5, transform.position, 0.8f);
    }

    private Click_Casilla GetNextCellWithMage()
    {
        int columns = GridManager.Instance.grid.GetLength(1);

        Click_Casilla closestCell = null;
        float closestDistance = float.MaxValue;

        for (int col = 0; col < columns; col++)
        {
            Click_Casilla cell = GridManager.Instance.grid[lane, col];

            if (cell != null && cell.isOccupied)
            {
                if (cell.transform.position.x < transform.position.x)
                {
                    float distance = transform.position.x - cell.transform.position.x;

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCell = cell;
                    }
                }
            }
        }

        return closestCell;
    }
}