using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public Click_Casilla[,] grid;

    private void Awake()
    {
        Instance = this;
    }

    public void InitializeGrid(int filas, int columnas)
    {
        grid = new Click_Casilla[filas, columnas];
    }

    public void RegisterCell(Click_Casilla cell)
    {
        grid[cell.Fila, cell.Columna] = cell;
    }

    public Click_Casilla GetCell(int fila, int columna)
    {
        return grid[fila, columna];
    }

}
