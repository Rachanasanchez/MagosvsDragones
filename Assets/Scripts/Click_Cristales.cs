using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase se encarga de detectar el click en los cristales, y de añadir soles al jugador por cada cristal recogido
public class Click_Cristales : MonoBehaviour
{
    [SerializeField] private int valorPorCristal = 50;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (cam == null) return;

        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if (hit.collider == null) return;

        // Si el objeto clicado tiene la tag "Cristal", lo recogemos
        if (hit.collider.CompareTag("Cristal"))
        {
            PlayerDataManager.Instance.SumarCristales(valorPorCristal);
            Destroy(hit.collider.gameObject);
        }
    }
}
