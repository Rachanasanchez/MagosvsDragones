using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// Este script se encarga de actualizar el texto que muestra la cantidad de cristales que el jugador tiene.

public class CristalesUI : MonoBehaviour
{
    private TextMeshPro cantidadCristales;

    private void Awake()
    {
        cantidadCristales = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        if (PlayerDataManager.Instance == null) return;
        cantidadCristales.text = PlayerDataManager.Instance.CristalesActuales.ToString();
    }
}
