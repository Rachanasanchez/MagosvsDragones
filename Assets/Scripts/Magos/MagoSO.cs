using UnityEngine;

//Clase que representa los datos de cada mago, como su tipo, prefab y coste. Se utiliza para crear ScriptableObjects que almacenan esta información de manera organizada y fácil de acceder en el editor de Unity.

[CreateAssetMenu(
    fileName = "MagoSO",
    menuName = "Magos/Nuevo_Mago",
    order = 0)]
public class MagoSO : ScriptableObject
{
    public MagosType magoType;
    public GameObject prefab;
    public int cost;
}

public enum MagosType
{
    Magos,
    Esqueleto//TFG
}
