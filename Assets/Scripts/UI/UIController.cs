using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("HUD")]
    [SerializeField] private GameObject hudObject;

    [Header("Pause")]
    [SerializeField] private GameObject pauseObject;





    #region UNITY CALLBACKS
    private void Awake()
    {
        UIController.instance = this;
    }

    private void Start()
    {
        hudObject.SetActive(true);
        pauseObject.SetActive(false);
    }

    private void OnGUI()
    {
        if (Application.isEditor || Debug.isDebugBuild) GUI.Label(new Rect(5, 5, 100, 25), ((int)(1f / Time.unscaledDeltaTime)).ToString());
    }
    #endregion





    #region CLASS METHODS
    public static void ShowPauseMenu()
    {
        instance.pauseObject.SetActive(true);
    }

    public static void HidePauseMenu()
    {
        instance.pauseObject.SetActive(false);
    }

    public void OnClick_ResumeButton()
    {
        GameController.Resume();
        instance.pauseObject.SetActive(false);
    }

    public void OnClick_RestartButton()
    {
        GameController.Restart();
    }

    public void OnClick_MainMenuButton()
    {
        GameController.MainMenu();
    }

    private IEnumerator FlashHPBar()
    {
        yield return null;
    }
    #endregion
}