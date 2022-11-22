using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { NONE, Seeker, Spinner, Tank }

public class Enemy : MonoBehaviour
{
    public static int layerMask = 1 << 8;

    [Header("Attributes")]
    [SerializeField] private EnemyType type;
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
        model.LookAt(new Vector3(Player.instance.transform.position.x, model.position.y, Player.instance.transform.position.z));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) 
        {
            EnemyController.OnEnemyDefeated();
            gameObject.SetActive(false);
        }

        // else give feedback
    }
}