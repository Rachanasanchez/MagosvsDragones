using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public static LaneManager Instance;
    public static Creacion_Casillas creacion_casillas;

    private List<Zombies>[] zombiesPerLane;

    private void Start()
    {
        Instance = this;

        //zombiesPerLane = new List<Zombie>[creacion_casillas.rows];
        //for (int i = 0; i < creacion_casillas.rows; i++)
            //zombiesPerLane[i] = new List<Zombie>();

        zombiesPerLane = new List<Zombies>[5];
        for (int i = 0; i < 5; i++)
            zombiesPerLane[i] = new List<Zombies>();
    }

    public void RegisterZombie(Zombies z, int lane)
    {
        zombiesPerLane[lane].Add(z);
    }

    public void RemoveZombie(Zombies z, int lane)
    {
        zombiesPerLane[lane].Remove(z);
    }


    //hacer booleano para saber si hay zombies en la fila
    public Zombies GetFirstZombie(int lane)
    {
        if (zombiesPerLane[lane].Count == 0) return null;

        return zombiesPerLane[lane][0];
    }
}
