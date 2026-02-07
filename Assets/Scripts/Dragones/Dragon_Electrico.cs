using UnityEngine;

public class Dragon_Electrico : Dragones
{
    private Click_Casilla targetCell;

    [Header("Distancia para atacar")]
    public float attackRange;

    private void Update()
    {
        // Buscar planta si no hay objetivo
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
            // No hay planta -> caminar
            WalkForward();
        }
    }

    // Movimiento y ataque
    
    private void AttackBehavior()
    {
        Vector3 targetPos = targetCell.transform.position;
        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance > attackRange)
        {
            // Caminar hacia la planta
            transform.position = Vector3.MoveTowards(transform.position,
                                                     new Vector3(targetPos.x, transform.position.y, transform.position.z),
                                                     speed * Time.deltaTime);
            gameObject.GetComponent<Animator>().SetBool("Atacar", false);
        }
        else
        {
            //Debug.Log("Estoy en rango y pongo Atacar = true");
            // Frente a la planta -> atacar
            gameObject.GetComponent<Animator>().SetBool("Atacar", true);
        }
    }

    /*
    private void AttackBehavior()
    {
        if (targetCell == null)
        {
            WalkForward();
            return;
        }

        Vector3 targetPos = targetCell.transform.position;

        // Si la he pasado, busco otra PERO SIGO EJECUTANDO
        if (transform.position.x < targetPos.x)
        {
            targetCell = GetNextCellWithPlant();

            if (targetCell == null)
            {
                WalkForward();
                return;
            }

            targetPos = targetCell.transform.position;
        }

        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance > attackRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetPos.x, transform.position.y, transform.position.z),
                speed * Time.deltaTime
            );

            GetComponent<Animator>().SetBool("Atacar", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Atacar", true);
        }
    }
    */
    private void WalkForward()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        GetComponent<Animator>().SetBool("Atacar", false);
    }


    public void Hacer_Dano()
    {
        if (targetCell == null || !targetCell.isOccupied) return;

        Magos mago = targetCell.magoEnCasilla;

        if (mago == null)
        {
            targetCell.Liberar();
            targetCell = null;
            return;
        }

        mago.TakeDamage(attack);
        audioSource.PlayOneShot(audioAtaque);

        if (!targetCell.isOccupied)
            targetCell = null;
    }


    public void sonidoVolar()
    {
        audioSource.PlayOneShot(audioVolar);
    }

    // Devuelve la primera celda de la línea que tenga planta
    private Click_Casilla GetNextCellWithPlant()
    {
        int columns = GridManager.Instance.grid.GetLength(1);

        for (int col = 0; col < columns; col++)
        {
            Click_Casilla cell = GridManager.Instance.grid[lane, col];

            if (cell != null && cell.isOccupied)
            {
                // SOLO si está por delante del dragón
                if (cell.transform.position.x < transform.position.x)
                {
                    return cell;
                }
            }
        }

        return null;
    }

}
