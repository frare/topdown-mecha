using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void OnClick_RestartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClick_MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}