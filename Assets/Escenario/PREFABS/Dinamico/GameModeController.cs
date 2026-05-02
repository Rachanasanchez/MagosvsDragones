using UnityEngine;

public class GameModeController : MonoBehaviour
{
    public enum GameMode
    {
        Level,
        Infinite
    }

    [Header("Mode")]
    public GameMode selectedMode = GameMode.Level;

    [Header("Level Settings")]
    public float[] levelDurations = new float[3] { 60f, 120f, 180f };
    public int selectedLevel = 0;

    [Range(0.5f, 5f)]
    public float difficultyMultiplier = 1f;

    [Header("References")]
    public DynamicLevelDirector director;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        if (selectedMode == GameMode.Level)
        {
            StartLevelMode();
        }
        else
        {
            StartInfiniteMode();
        }
    }

    void StartLevelMode()
    {
        float duration = levelDurations[Mathf.Clamp(selectedLevel, 0, levelDurations.Length - 1)];

        Debug.Log("START LEVEL MODE | Level: " + selectedLevel + " | Duration: " + duration);

        director.StartLevelMode(duration, difficultyMultiplier);
    }

    void StartInfiniteMode()
    {
        Debug.Log("START INFINITE MODE");

        director.StartInfiniteMode(difficultyMultiplier);
    }
}