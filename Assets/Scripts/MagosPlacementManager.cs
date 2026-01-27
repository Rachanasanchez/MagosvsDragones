using UnityEngine;

public class MagosPlacementManager : MonoBehaviour
{
    public static MagosPlacementManager Instance { get; private set; }

    [Header("Opcional")]
    [SerializeField] private KeyCode cancelKey = KeyCode.Escape;

    private Camera camara;
    private GameObject magoSeleccionado;
    private int costeSeleccion;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        camara = Camera.main;
        if (camara == null)
            Debug.LogError("PlantPlacementManager: No existe Camera.main (tag MainCamera).");
    }

    public void BeginPlacement(GameObject magoPrefab, int coste)
    {
        // Si ya hay un mago “en mano”, la reemplazamos
        if (magoSeleccionado != null)
            Destroy(magoSeleccionado);

        magoSeleccionado = Instantiate(magoPrefab);
        magoSeleccionado.name = magoPrefab.name + "_Dragging";
        costeSeleccion = coste;

    }

    private void Update()
    {
        if (magoSeleccionado == null) return;

        FollowMouse(magoSeleccionado);

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
        mouse.z = -camara.transform.position.z; // para 2D ortho suele funcionar bien
        Vector3 world = camara.ScreenToWorldPoint(mouse);
        world.z = 0f;

        obj.transform.position = world;
    }

    private void TryPlaceOnCell()
    {
        Vector2 worldPoint = camara.ScreenToWorldPoint(Input.mousePosition);
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

        PlayerDataManager.Instance.SpendSun(costeSeleccion);

        // Colocar: hija de la celda y centrada
        magoSeleccionado.transform.SetParent(cell.transform, worldPositionStays: false);
        magoSeleccionado.transform.localPosition = Vector3.zero;

        // settear todo del mago
        Magos mago = magoSeleccionado.GetComponent<Magos>();
        mago.Initialize(cell);

        // OJO: ocupa la casilla guardando referencia al mago
        cell.Ocupar(mago);

        magoSeleccionado = null; // ya no estamos colocando
    }

    private void CancelPlacement()
    {
        if (magoSeleccionado != null)
            Destroy(magoSeleccionado);

        magoSeleccionado = null;
    }
}
