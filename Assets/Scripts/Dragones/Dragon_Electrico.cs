using UnityEngine;

public class Dragon_Electrico : Zombies
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
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            gameObject.GetComponent<Animator>().SetBool("Atacar", false);
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
            // Frente a la planta -> atacar
            gameObject.GetComponent<Animator>().SetBool("Atacar", true);
        }
    }

    public void Hacer_Dano_Mago()
    {

    }

    public void Eliminar_Al_Mago()
    {

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
                return cell;
            }
        }

        return null; // No hay plantas en la línea
    }
}
