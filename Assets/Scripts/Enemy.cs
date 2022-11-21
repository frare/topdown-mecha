using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public enum EnemyType { NONE, Basic }

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

    [Header("Sounds")]
    [SerializeField, EventRef] private string onTakeDamage;
    [SerializeField, EventRef] private string onDeath;




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
            RuntimeManager.PlayOneShot(onDeath);
            gameObject.SetActive(false);
        }
        else
        {
            RuntimeManager.PlayOneShotAttached(onTakeDamage, gameObject);
        }
    }
}