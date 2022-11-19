using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float disableTime;
    private float currentDisableTime;

    [Header("References")]
    [SerializeField] private Rigidbody rb;





    private void OnEnable()
    {
        currentDisableTime = 0f;
    }

    private void Update()
    {
        rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);

        if (currentDisableTime >= disableTime) gameObject.SetActive(false);
        else currentDisableTime += Time.deltaTime;
    }
}