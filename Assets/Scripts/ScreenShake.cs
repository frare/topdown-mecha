using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Coroutine shakeCoroutine;



    public void Shake(float intensity, float duration)
    {
        StopShake();
        shakeCoroutine = StartCoroutine(ShakeCoroutine(intensity, duration));
    }

    public void StopShake()
    {
        if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);
        transform.localPosition = Vector3.zero;
    }

    private IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            
            transform.localPosition = Random.insideUnitCircle * intensity;

            yield return null;
        }

        transform.localPosition = Vector3.zero;
    }
}