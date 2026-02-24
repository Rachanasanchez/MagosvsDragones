using System.Collections.Generic;
using UnityEngine;

// Este script se encarga de gestionar los dragones en cada línea. Es un singleton para que pueda ser accedido desde cualquier parte del juego sin necesidad de referencias directas.
public class LaneManager : MonoBehaviour
{
    public static LaneManager Instance;

    private List<Dragones>[] dragonesPorLinea;

    private void Start()
    {
        Instance = this;

        dragonesPorLinea = new List<Dragones>[Creacion_Casillas.Instancia.GetRows()];
        for (int i = 0; i < Creacion_Casillas.Instancia.GetRows(); i++)
            dragonesPorLinea[i] = new List<Dragones>();

    }

    public void RegistrarDragon(Dragones z, int lane)
    {
        dragonesPorLinea[lane].Add(z);
    }

    public void QuitarDragon(Dragones z, int lane)
    {
        dragonesPorLinea[lane].Remove(z);
    }


    //hacer booleano para saber si hay dragones en la fila
    public Dragones ObtenerPrimerDragon(int lane, float posicionAtacanteX)
    {
        if(lane == -1)
        {
            return null;
        }

        if (dragonesPorLinea[lane].Count == 0)
            return null;

        foreach (Dragones dragon in dragonesPorLinea[lane])
        {
            // Si el dragón está a la derecha del atacante
            if (dragon.transform.position.x > posicionAtacanteX)
            {
                return dragon;
            }
        }

        // Si ninguno está delante
        return null;
    }

}
