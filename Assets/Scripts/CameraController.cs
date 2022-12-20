using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Attributes")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float zoomDistance;





    private void Awake()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        transform.position = Player.GetPosition() + offset + (-transform.forward * zoomDistance);
    }

    public static void ChangeZoom(float distance) 
    {
        instance.StopCoroutine(instance.ChangeZoomCoroutine(distance));
        instance.StartCoroutine(instance.ChangeZoomCoroutine(distance));
    }

    private IEnumerator ChangeZoomCoroutine(float distance)
    {
        float initialZoom = zoomDistance;
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;

            zoomDistance = Mathf.Lerp(initialZoom, distance, time);

            yield return null;
        }
    }
}