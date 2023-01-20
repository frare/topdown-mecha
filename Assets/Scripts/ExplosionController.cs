using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public static ExplosionController instance;

    [SerializeField] private ObjectPool pool;
    [SerializeField] private Vector3 offset;





    private void Awake()
    {
        ExplosionController.instance = this;
    }

    public static void SpawnExplosion(Vector3 position, Vector3 scale)
    {
        var explosion = instance.pool.GetNext();
        explosion.transform.position = position + instance.offset;
        explosion.transform.localScale = scale;
        explosion.SetActive(true);
    }
}