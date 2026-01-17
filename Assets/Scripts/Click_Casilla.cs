using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Click_Casilla : MonoBehaviour
{

    [Header("State")]
    public bool isOccupied;
    public bool isAvailable;

    [Header("Cost")]
    public int cost;

    // Propiedades públicas (seguras)
    public bool IsOccupied => isOccupied;
    public bool IsAvailable => isAvailable;
    public int Cost => cost;

    private void OnMouseDown()
    {
        if (!isAvailable)
        {
            Debug.Log($"{name} NO disponible");
            return;
        }

        if (isOccupied)
        {
            Debug.Log($"{name} ya está OCUPADA");
            return;
        }

        Debug.Log($"Click en {name} | Coste: {cost}");
    }

    public void SetOccupied(bool value)
    {
        isOccupied = value;
    }

    public void SetAvailable(bool value)
    {
        isAvailable = value;
    }

    public void SetCost(int newCost)
    {
        cost = newCost;
    }
}
