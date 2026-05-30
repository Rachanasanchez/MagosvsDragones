using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPanelNiveles : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelNiveles;

    public void AbrirNiveles()
    {
        panelMenu.SetActive(false);
        panelNiveles.SetActive(true);
    }
}
