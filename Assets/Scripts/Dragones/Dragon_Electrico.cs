using UnityEngine;

public class Dragon_Electrico : Dragones
{
    private Click_Casilla targetCell;

    [Header("Distancia para atacar")]
    public float attackRange;

    private void Update()
    {
        targetCell = GetNextCellWithPlant();

        if (targetCell != null && targetCell.isOccupied)
        {
            AttackBehavior();
        }
        else
        {
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
        SoundManager.Instance.PlayLimited(audioAtaque, 5, transform.position, 0.8f);


        if (!targetCell.isOccupied)
            targetCell = null;
    }


    public void sonidoVolar()
    {
        SoundManager.Instance.PlayLimited(audioVolar, 5, transform.position, 0.8f);

    }

    // Devuelve la primera celda de la línea que tenga planta
    private Click_Casilla GetNextCellWithPlant()
    {
        int columns = GridManager.Instance.grid.GetLength(1);

        Click_Casilla closestCell = null;
        float closestDistance = float.MaxValue;

        for (int col = 0; col < columns; col++)
        {
            Click_Casilla cell = GridManager.Instance.grid[lane, col];

            if (cell != null && cell.isOccupied)
            {
                // Solo si está por delante del dragón
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
