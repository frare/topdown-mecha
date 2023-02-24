using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;





    private void OnEnable()
    {
        particles.Play();
        
        CameraController.LightShake();
    }

    private void Update()
    {
        if (particles.isStopped) gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        particles.Stop();
    }
}