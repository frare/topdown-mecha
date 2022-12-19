using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("HUD")]
    [SerializeField] private GameObject hudObject;
    [SerializeField] private List<GameObject> healthBars;
    private Coroutine healthBarCoroutine;

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

    public static void UpdatePlayerHealth(int playerHealth)
    {
        if (instance.healthBarCoroutine != null) instance.StopCoroutine(instance.healthBarCoroutine);
        instance.healthBarCoroutine = instance.StartCoroutine(instance.FlashPlayerHealth(playerHealth));
    }

    private IEnumerator FlashPlayerHealth(int playerHealth)
    {
        playerHealth = Mathf.Clamp(playerHealth, 0, 3);
        for (int i = 0; i < healthBars.Count; i++) healthBars[i].SetActive(i < playerHealth);

        if (playerHealth != 0 && playerHealth != 3)
        {
            for (float time = 0; time < 3f; time += .2f)
            {
                healthBars[playerHealth].SetActive(true);
                yield return new WaitForSeconds(.1f);

                healthBars[playerHealth].SetActive(false);
                yield return new WaitForSeconds(.1f);
            }
        }
    }
    #endregion
}