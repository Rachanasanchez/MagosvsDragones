using UnityEngine;
using UnityEngine.SceneManagement;

//Clase encargada de manejar la escena de créditos, permitiendo volver al menú principal
public class CreditosManager : MonoBehaviour
{
    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
