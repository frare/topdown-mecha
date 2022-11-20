using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Attributes")]
    [SerializeField] private Vector3 offset;





    private void Awake()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        transform.position = Player.instance.transform.position + offset;
    }
}