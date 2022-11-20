using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int layerMask = 1 << 8;

    [Header("Attributes")]
    [SerializeField] private int health;
    private int currentHealth;
    [SerializeField] private float moveSpeed;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform model;





    private void OnEnable()
    {
        currentHealth = health;
    }

    private void Update()
    {
        rb.MovePosition(transform.position + (Player.instance.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
        model.LookAt(Player.instance.transform.position);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) gameObject.SetActive(false);

        // else give feedback
    }
}