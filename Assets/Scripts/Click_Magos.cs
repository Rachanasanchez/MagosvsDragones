using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Click_Magos : MonoBehaviour
{
    public MagoSO magoPrefab;

    private void OnMouseDown()
    {
        if (magoPrefab == null)
        {
            Debug.Log("PlantPicker: No hay plantPrefab asignado.");
            return;
        }

        if (PlayerDataManager.Instance.HasEnoughSun(magoPrefab.cost))
        {
            PlantPlacementManager.Instance.BeginPlacement(magoPrefab.prefab, magoPrefab.cost);
            return;
        }
        else
        {
            Debug.Log("No tienes suficientes soles.");

        }

    }
}
