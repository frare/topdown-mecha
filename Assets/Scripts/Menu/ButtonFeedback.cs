using UnityEngine;
using UnityEngine.EventSystems;
// using FMODUnity;

public class ButtonFeedback : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // RuntimeManager.PlayOneShot("event:/Menu/Button_Hover");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // RuntimeManager.PlayOneShot("event:/Menu/Button_Click");
    }
}