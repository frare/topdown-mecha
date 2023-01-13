using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { NONE, Seeker, Spinner, Tank }

/// <summary> Base class for all the enemies. </summary>
public class Enemy : MonoBehaviour
{
    public static readonly int layer = 8;
    public static readonly int layerMask = 1 << 8;

    [Header("Attributes")]
    [SerializeField] protected EnemyType type;
    [SerializeField] protected int health;
    protected int currentHealth;
    [SerializeField] protected float moveSpeed;
    protected float currentMoveSpeed;
    protected bool isElite;

    [Header("References")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform model;
    [SerializeField] protected Animator animator;
    [SerializeField] protected FlashBehaviour flash;





    protected virtual void OnEnable()
    {
        currentHealth = health;
        currentMoveSpeed = moveSpeed;
        flash.ResetMaterials();
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
        else
        {
            StopCoroutine(flash.FlashOnce(.1f));
            StartCoroutine(flash.FlashOnce(.1f));
        }
    }

    public virtual void SetElite()
    {
        isElite = true;
        health = health * (int)(DifficultyManager.difficulty * 3);
        currentHealth = health;
        moveSpeed = moveSpeed / (DifficultyManager.difficulty * 3);
        currentMoveSpeed = moveSpeed;

        StopCoroutine(SetEliteCoroutine());
        StartCoroutine(SetEliteCoroutine());
    }

    private IEnumerator SetEliteCoroutine()
    {
        float difficultyModifier = DifficultyManager.difficulty * 3f;
        float initialScale = model.localScale.x;
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime / 2.5f;

            model.localScale = Vector3.one * Mathf.Lerp(initialScale, difficultyModifier, time);
            if (animator != null) animator.speed = Mathf.Lerp(1f, 1f / difficultyModifier, time);

            yield return null;
        }

        model.localScale = Vector3.one * difficultyModifier;
        if (animator != null) animator.speed = 1f / difficultyModifier;
    }
}