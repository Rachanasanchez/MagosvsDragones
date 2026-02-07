using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunUI : MonoBehaviour
{
    private TextMeshPro cantidadCristales;

    private void Awake()
    {
        cantidadCristales = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        if (PlayerDataManager.Instance == null) return;
        cantidadCristales.text = PlayerDataManager.Instance.CurrentSun.ToString();
    }
}
