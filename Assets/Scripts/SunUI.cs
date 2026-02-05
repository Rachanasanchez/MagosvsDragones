using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class SunUI : MonoBehaviour
{
    private TextMeshProUGUI texto;

    private void Awake()
    {
        texto = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (PlayerDataManager.Instance == null) return;
        texto.text = PlayerDataManager.Instance.CurrentSun.ToString();
    }
}
