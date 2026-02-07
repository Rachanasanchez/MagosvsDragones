using System.Collections.Generic;
using UnityEngine;

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

        for (int i = 0; i < 5; i++)
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
    public Dragones ObtenerPrimerDragon(int lane)
    {
        if (dragonesPorLinea[lane].Count == 0) return null;

        return dragonesPorLinea[lane][0];
    }
}
