using UnityEngine;

// Esta clase se encarga de detectar el click en el icono del mago, y de iniciar el proceso de colocación del mago si el jugador tiene suficientes soles

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

        if (PlayerDataManager.Instance.TieneSuficientesCristales(magoPrefab.cost))
        {
            MagosPlacementManager.Instance.BeginPlacement(magoPrefab.prefab, magoPrefab.cost);
            return;
        }
        else
        {
            Debug.Log("No tienes suficientes soles.");

        }

    }
}
