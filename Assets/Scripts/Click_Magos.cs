using TMPro;
using UnityEngine;

// Esta clase se encarga de detectar el click en el icono del mago, y de iniciar el proceso de colocación del mago si el jugador tiene suficientes soles

[RequireComponent(typeof(Collider2D))]
public class Click_Magos : MonoBehaviour
{
    //public MagoSO magoPrefab;
    public Unidades unidad;

    private TextMeshPro textoCoste;

    private void Start()
    {
        textoCoste = GetComponentInChildren<TextMeshPro>();

        if (textoCoste == null)
        {
            Debug.LogError("Click_Magos: No se encontró un componente TextMeshPro en los hijos.");
            return;
        }

        if (unidad != null)
        {
            textoCoste.text = unidad.coste.ToString();
        }
    }

    private void OnMouseDown()
    {
        /*
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
        */

        if (unidad == null)
        {
            Debug.Log("PlantPicker: No hay plantPrefab asignado.");
            return;
        }

        if (PlayerDataManager.Instance.TieneSuficientesCristales(unidad.coste))
        {
            MagosPlacementManager.Instance.BeginPlacement(unidad.gameObject, unidad.coste);
            return;
        }
        else
        {
            Debug.Log("No tienes suficientes soles.");

        }

    }
}
