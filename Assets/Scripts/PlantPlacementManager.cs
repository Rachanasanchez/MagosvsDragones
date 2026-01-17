using UnityEngine;

public class PlantPlacementManager : MonoBehaviour
{
    public static PlantPlacementManager Instance { get; private set; }

    [Header("Opcional")]
    [SerializeField] private KeyCode cancelKey = KeyCode.Escape;

    private Camera cam;
    private GameObject draggingPlant;
    private int draggingCost;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        cam = Camera.main;
        if (cam == null)
            Debug.LogError("PlantPlacementManager: No existe Camera.main (tag MainCamera).");
    }

    public void BeginPlacement(GameObject plantPrefab, int coste)
    {
        // Si ya hay una planta “en mano”, la reemplazamos
        if (draggingPlant != null)
            Destroy(draggingPlant);

        draggingPlant = Instantiate(plantPrefab);
        draggingPlant.name = plantPrefab.name + "_Dragging";
        draggingCost = coste;

    }

    private void Update()
    {
        if (draggingPlant == null) return;

        FollowMouse(draggingPlant);

        if (Input.GetKeyDown(cancelKey))
        {
            CancelPlacement();
            return;
        }

        // Click izquierdo para intentar colocar
        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceOnCell();
        }
    }

    private void FollowMouse(GameObject obj)
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -cam.transform.position.z; // para 2D ortho suele funcionar bien
        Vector3 world = cam.ScreenToWorldPoint(mouse);
        world.z = 0f;

        obj.transform.position = world;
    }

    private void TryPlaceOnCell()
    {
        Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (!hit.collider)
        {
            Debug.Log("No has clicado una celda.");
            return;
        }

        Click_Casilla cell = hit.collider.GetComponent<Click_Casilla>();
        if (cell == null)
        {
            Debug.Log("Has clicado algo que no es una Cell.");
            return;
        }

        if (!cell.IsAvailable)
        {
            Debug.Log($"{cell.name} NO disponible");
            return;
        }

        if (cell.IsOccupied)
        {
            Debug.Log($"{cell.name} ya está OCUPADA");
            return;
        }        

        PlayerDataManager.Instance.SpendSun(draggingCost);

        // Colocar: hija de la celda y centrada
        draggingPlant.transform.SetParent(cell.transform, worldPositionStays: false);
        draggingPlant.transform.localPosition = Vector3.zero;

        cell.SetOccupied(true);

        Debug.Log($"Planta colocada en {cell.name} | Coste: {draggingCost}");

        draggingPlant = null; // ya no estamos colocando
    }

    private void CancelPlacement()
    {
        if (draggingPlant != null)
            Destroy(draggingPlant);

        draggingPlant = null;
        Debug.Log("Colocación cancelada.");
    }
}
