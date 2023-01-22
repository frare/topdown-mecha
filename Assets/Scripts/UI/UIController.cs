using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("HUD")]
    [SerializeField] private GameObject hudObject;
    [SerializeField] private LayoutGroup healthBarGroup;
    [SerializeField] private GameObject healthBarPrefab;
    private List<Image> healthBars = new List<Image>();
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
        StartCoroutine(InstantiateHealthBars());

        hudObject.SetActive(true);
        pauseObject.SetActive(false);
    }

    private void OnGUI()
    {
        // show framerate counter
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

    private IEnumerator InstantiateHealthBars()
    {
        for (int i = 0; i < healthBarGroup.transform.childCount; i++) Destroy(healthBarGroup.transform.GetChild(i).gameObject);

        healthBarGroup.enabled = true;
        for (int j = 0; j < Player.health; j++)
        {
            var bar = Instantiate(healthBarPrefab, healthBarGroup.transform);
            healthBars.Add(bar.GetComponent<Image>());
        }

        yield return null;
        healthBarGroup.enabled = false;
    }

    public static void UpdatePlayerHealth(int playerHealth)
    {
        if (instance.healthBarCoroutine != null) instance.StopCoroutine(instance.healthBarCoroutine);
        instance.healthBarCoroutine = instance.StartCoroutine(instance.FlashPlayerHealth(playerHealth));
    }

    private IEnumerator FlashPlayerHealth(int playerHealth)
    {
        playerHealth = Mathf.Clamp(playerHealth, 0, healthBars.Count);
        for (int i = 0; i < healthBars.Count; i++) healthBars[i].enabled = i < playerHealth;

        if (playerHealth != 0 && playerHealth != healthBars.Count)
        {
            for (float time = 0; time < 3f; time += .2f)
            {
                healthBars[playerHealth].enabled = true;
                yield return new WaitForSeconds(.1f);

                healthBars[playerHealth].enabled = false;;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
    #endregion
}