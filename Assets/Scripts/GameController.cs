using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool isPaused { get; private set; } = false;



    private void Start()
    {
        Time.timeScale = 1f;

        EnemyController.SpawnNextWave();
    }

    public static void Pause()
    {
        UIController.ShowPauseMenu();
        isPaused = true;
        Time.timeScale = 0f;
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        UIController.HidePauseMenu();
        isPaused = false;
    }

    public static void Restart()
    {
        SceneManager.LoadScene("02-Game", LoadSceneMode.Single);
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene("01-MainMenu", LoadSceneMode.Single);
    }
}