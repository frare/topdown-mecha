using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { NONE, Seeker, Spinner, Tank }

/// <summary> Base class for all the enemies. </summary>
public class Enemy : MonoBehaviour
{
    public static int layer = 8;
    public static int layerMask = 1 << 8;

    [Header("Attributes")]
    [SerializeField] protected EnemyType type;
    [SerializeField] protected int health;
    protected int currentHealth;
    [SerializeField] protected float moveSpeed;
    protected float currentMoveSpeed;

    [Header("References")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform model;





    protected virtual void OnEnable()
    {
        currentHealth = health;
        currentMoveSpeed = moveSpeed;
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    protected virtual void FixedUpdate()
    {
        rb.MovePosition(transform.position + (Player.GetPosition() - transform.position).normalized * currentMoveSpeed * Time.deltaTime);
        model.LookAt(new Vector3(Player.GetPosition().x, model.position.y, Player.GetPosition().z));
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Player.layer) other.attachedRigidbody.GetComponent<Player>()?.TakeDamage(1);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) 
        {
            EnemyController.OnEnemyDefeated();
            gameObject.SetActive(false);
        }
    }
}