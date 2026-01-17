using UnityEngine;

[CreateAssetMenu(
    fileName = "MagoSO",
    menuName = "Magos/Nuevo_Mago",
    order = 0)]
public class MagoSO : ScriptableObject
{
    public MagosType plantType;
    public GameObject prefab;
    public int cost;
}

public enum MagosType
{
    Magos,
    Esqueleto
}
