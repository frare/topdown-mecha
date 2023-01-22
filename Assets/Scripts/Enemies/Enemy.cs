using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType { NONE, Seeker, Spinner, Tank }

/// <summary> Base class for all the enemies. </summary>
public class Enemy : MonoBehaviour
{
    public static readonly int layer = 8;
    public static readonly int layerMask = 1 << 8;

    #region EVENTS
    public delegate void EnemyDefeated(Enemy enemy);
    public event EnemyDefeated OnEnemyDefeated;
    #endregion

    [Header("Attributes")]
    [SerializeField] protected EnemyType type;
    [SerializeField] protected int maxHealth;
    [ReadOnly] [SerializeField] protected int currentHealth;
    [SerializeField] protected float moveSpeed;
    protected float currentMoveSpeed 
    { 
        get { return navMeshAgent.speed; } 
        set { navMeshAgent.speed = value; }
    }
    protected bool isElite;

    [Header("References")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform model;
    [SerializeField] protected Animator animator;
    [SerializeField] protected FlashBehaviour flash;
    [SerializeField] protected NavMeshAgent navMeshAgent;





    #region VIRTUAL METHODS
    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
        currentMoveSpeed = moveSpeed;
        flash.ResetMaterials();
        navMeshAgent.updateRotation = false;
        navMeshAgent.speed = currentMoveSpeed;
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    protected virtual void FixedUpdate()
    {
        navMeshAgent.destination = Player.position;
        // rb.MovePosition(transform.position + (Player.position - transform.position).normalized * currentMoveSpeed * Time.deltaTime);
        model.LookAt(new Vector3(Player.position.x, model.position.y, Player.position.z));
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Player.layer) other.attachedRigidbody.GetComponent<Player>()?.TakeDamage(1);
    }
    #endregion



    

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) 
        {
            ExplosionController.SpawnExplosion(transform.position, model.localScale);
            gameObject.SetActive(false);
            if (OnEnemyDefeated != null) OnEnemyDefeated(this);
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
        maxHealth = maxHealth * (int)(DifficultyManager.difficulty * 3);
        currentHealth = maxHealth;
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