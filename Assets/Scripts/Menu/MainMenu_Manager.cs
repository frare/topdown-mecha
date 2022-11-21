using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public void PlayGame() => StartCoroutine(OnPlayGame());
    public void QuitGame() => Application.Quit();

    private IEnumerator OnPlayGame()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
        SceneManager.LoadScene(1);
    }
}