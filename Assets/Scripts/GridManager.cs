using System;
using UnityEngine;

// Este script se encarga de gestionar la cuadrícula de casillas. Es un singleton para que pueda ser accedido desde cualquier parte del juego sin necesidad de referencias directas.
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
