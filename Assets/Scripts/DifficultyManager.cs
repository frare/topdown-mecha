using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;

    [SerializeField] private float currentDifficulty = 1f;
    [SerializeField] private float difficultyIncreasePerWave;
    public static float difficulty 
    {
        get { return instance.currentDifficulty; }
        private set { instance.currentDifficulty = Mathf.Clamp(value, 1f, 100f); } 
    }





    private void Awake()
    {
        DifficultyManager.instance = this;
    }

    public static void Reset()
    {
        difficulty = 1f;
    }

    public static void IncreaseDifficulty()
    {
        difficulty += instance.difficultyIncreasePerWave;
        CameraController.ChangeZoom(difficulty);
    }
}
