using UnityEngine;

public class Creacion_Casillas : MonoBehaviour
{
    [Header("Prefab de la casilla")]
    public GameObject cellPrefab;

    [Header("Tamaño del grid")]
    public int rows;
    public int columns;

    [Header("Espaciado")]
    public float spacingX;
    public float spacingY;

    [Header("Centrar grid")]
    public bool centerGrid = true;

    private Transform container;

    private void Start()
    {
        CreateContainer();
        SpawnGrid();
    }

    private void CreateContainer()
    {
        GameObject go = new GameObject("Cells");
        go.transform.SetParent(transform);
        container = go.transform;
    }

    private void SpawnGrid()
    {
        if (cellPrefab == null)
        {
            Debug.LogError("No hay prefab asignado.");
            return;
        }

        float width = (columns - 1) * spacingX;
        float height = (rows - 1) * spacingY;

        Vector3 offset = centerGrid
            ? new Vector3(-width / 2f, height / 2f, 0f)
            : Vector3.zero;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = transform.position
                                 + offset
                                 + new Vector3(col * spacingX, -row * spacingY, 0f);

                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, container);
                cell.name = $"Cell_{row + 1}_{col + 1}";
            }
        }
    }
}
