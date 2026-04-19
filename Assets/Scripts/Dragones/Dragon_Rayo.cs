using System.Linq;
using UnityEngine;

public class Dragon_Rayo : Dragones
{
    public GameObject rayoPrefab;
    public GameObject rayoPermanentePrefab;
    private Click_Casilla targetCell;

    [Header("Distancia para atacar")]
    public float attackRange;

    private void Update()
    {
        targetCell = GetNextCellWithMage();

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

    private void WalkForward()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        GetComponent<Animator>().SetBool("Atacar", false);
    }

    public void Hacer_Dano()
    {
        if (targetCell == null || !targetCell.isOccupied) return;

        Unidades unidadObjetivo = targetCell.magoEnCasilla;

        if (unidadObjetivo == null)
        {
            targetCell.Liberar();
            targetCell = null;
            return;
        }

        Click_Casilla[] casillasFila = FindObjectsOfType<Click_Casilla>()
            .Where(c => c.Fila == targetCell.Fila)
            .OrderBy(c => c.Columna)
            .ToArray();

        int posicionObjetivo = System.Array.IndexOf(casillasFila, targetCell);
        if (posicionObjetivo == -1) return;

        // Casilla inicial
        AtacarCasilla(casillasFila[posicionObjetivo]);

        // Hacia la izquierda
        int izquierda = posicionObjetivo - 1;
        while (izquierda >= 0 && casillasFila[izquierda].isOccupied)
        {
            AtacarCasilla(casillasFila[izquierda]);
            izquierda--;
        }

        // Hacia la derecha
        int derecha = posicionObjetivo + 1;
        while (derecha < casillasFila.Length && casillasFila[derecha].isOccupied)
        {
            AtacarCasilla(casillasFila[derecha]);
            derecha++;
        }

        SoundManager.Instance.PlayLimited(audioAtaque, 5, transform.position, 0.8f);

        if (!targetCell.isOccupied)
            targetCell = null;
    }

    private void AtacarCasilla(Click_Casilla casilla)
    {
        if (casilla == null || !casilla.isOccupied) return;

        Unidades unidad = casilla.magoEnCasilla;
        if (unidad == null) return;

        Instantiate(rayoPrefab, casilla.transform.position, Quaternion.identity);

        float vidaAntes = unidad.vida;

        unidad.TakeDamage(attack);

        // Si murió con este golpe
        if (vidaAntes > 0 && unidad.vida <= 0)
        {
            GameObject rayoObj = Instantiate(rayoPermanentePrefab, casilla.transform.position, Quaternion.identity);

            RayoPermanente rayo = rayoObj.GetComponent<RayoPermanente>();
            if (rayo != null)
            {
                rayo.Inicializar(casilla);
            }
        }
    }

    public void sonidoVolar()
    {
        SoundManager.Instance.PlayLimited(audioVolar, 5, transform.position, 0.8f);

    }

    // Devuelve la primera celda de la línea que tenga planta
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
