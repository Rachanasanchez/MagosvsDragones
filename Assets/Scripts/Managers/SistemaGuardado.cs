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

    public static void ReiniciarProgreso()
    {
        for (int i = 1; i <= TOTAL_LEVELS; i++)
        {
            PlayerPrefs.DeleteKey("Level_" + i);
        }

        PlayerPrefs.DeleteKey("Infinito_RecordTiempo");
        PlayerPrefs.DeleteKey("Infinito_RecordEnemigos");

        PlayerPrefs.Save();

        Debug.Log("Progreso reiniciado");
    }

    // NIVEL INFINITO

    public static void GuardarRecordTiempoInfinito(float tiempo)
    {
        float recordActual = PlayerPrefs.GetFloat("Infinito_RecordTiempo", 0f);

        if (tiempo > recordActual)
        {
            PlayerPrefs.SetFloat("Infinito_RecordTiempo", tiempo);
            PlayerPrefs.Save();
        }
    }

    public static float ObtenerRecordTiempoInfinito()
    {
        return PlayerPrefs.GetFloat("Infinito_RecordTiempo", 0f);
    }

    public static void GuardarRecordEnemigosInfinito(int enemigos)
    {
        int recordActual = PlayerPrefs.GetInt("Infinito_RecordEnemigos", 0);

        if (enemigos > recordActual)
        {
            PlayerPrefs.SetInt("Infinito_RecordEnemigos", enemigos);
            PlayerPrefs.Save();
        }
    }

    public static int ObtenerRecordEnemigosInfinito()
    {
        return PlayerPrefs.GetInt("Infinito_RecordEnemigos", 0);
    }

    public static bool EsNuevoRecord(string tiempoActual)
    {
        string recordGuardado = PlayerPrefs.GetString("NivelInfinito", "00:00");

        int actualSegundos = ConvertirASegundos(tiempoActual);
        int recordSegundos = ConvertirASegundos(recordGuardado);

        return actualSegundos > recordSegundos;
    }

    private static int ConvertirASegundos(string tiempo)
    {
        string[] partes = tiempo.Split(':');

        int minutos = int.Parse(partes[0]);
        int segundos = int.Parse(partes[1]);

        return minutos * 60 + segundos;
    }

    public static void GuardarTiempoNivelInfinito(string contador)
    {
        if (EsNuevoRecord(contador))
        {
            PlayerPrefs.SetString("NivelInfinito", contador);
            PlayerPrefs.Save();

            Debug.Log("¡Nuevo récord! " + contador);
        }
        else
        {
            Debug.Log("No superaste el récord.");
        }
    }
}
