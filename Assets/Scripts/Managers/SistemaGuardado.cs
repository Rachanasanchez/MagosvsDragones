using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemaGuardado : MonoBehaviour
{
    private const int TOTAL_LEVELS = 4;

    public static void GuardarNivelCompletado()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Busca números dentro del nombre
        Match match = Regex.Match(sceneName, @"\d+");

        if (match.Success)
        {
            int level = int.Parse(match.Value);

            CompleteLevel(level);
        }
        else
        {
            Debug.LogError("La escena no tiene número de nivel");
        }
    }

    public static void CompleteLevel(int level)
    {
        if (level < 1 || level > TOTAL_LEVELS)
        {
            Debug.LogError("Nivel inválido");
            return;
        }

        PlayerPrefs.SetInt("Level_" + level, 1);
        PlayerPrefs.Save();

        Debug.Log("Nivel " + level + " completado");
    }

    public static bool IsLevelCompleted(int level)
    {
        return PlayerPrefs.GetInt("Level_" + level, 0) == 1;
    }

    public static bool IsLevelUnlocked(int level)
    {
        if (level == 1)
            return true;

        return IsLevelCompleted(level - 1);
    }

}
