using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static readonly int layer = 7;
    public static Vector3 position { get { return instance.transform.position; } private set { instance.transform.position = value; } }

    [Header("Attributes")]
    [SerializeField] private int health;
    [ReadOnly] [SerializeField] private int currentHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rangedCooldown;
    [ReadOnly] [SerializeField] private float currentRangedCooldown;
    [SerializeField] private float meleeCooldown;
    [ReadOnly] [SerializeField] private float currentMeleeCooldown;
    [SerializeField] private float meleeRange;
    [SerializeField] private float meleeSize;
    [SerializeField] private bool invulnerable;
    [SerializeField] private float invulnerabilityTime;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject bulletPoolPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private FlashBehaviour flash;
    [SerializeField] private List<ParticleSystem> thrusterParticles;
    [SerializeField] private float thrusterMinimumSize;
    [SerializeField] private ParticleSystem muzzleParticles;

    private ObjectPool bulletPool;
    private Vector3 moveInput;
    private Quaternion lookInput;
    private const int rotateRaycastLayerMask = 1 << 6;
    private Vector2 walkTreeVelocity;






    #region UNITY CALLBACKS
    private void Awake()
    {
        Player.instance = this;

        currentHealth = health;
        currentRangedCooldown = 0;
        currentMeleeCooldown = 0;
        invulnerable = false;

        bulletPool = Instantiate(bulletPoolPrefab.gameObject).GetComponent<ObjectPool>();
    }

    private void Update()
    {
        if (GameController.isPaused) return;

        rb.AddForce(moveInput * moveSpeed * 100 * Time.deltaTime);
        var thrusterSize = Mathf.Lerp(thrusterMinimumSize, 1f, rb.velocity.magnitude / 15);
        foreach (ParticleSystem thruster in thrusterParticles) 
            thruster.transform.localScale = new Vector3(1f, 1f, thrusterSize);

        if (currentRangedCooldown > 0) currentRangedCooldown -= Time.deltaTime;
        if (currentMeleeCooldown > 0) currentMeleeCooldown -= Time.deltaTime;
    }

    private void OnValidate()
    {
        currentHealth = health;
    }
    #endregion





    #region INPUT SYSTEM ACTION CALLBACKS
    public void Move(InputAction.CallbackContext context)
    {
        if (GameController.isPaused) return;

        if (context.performed)
        {
            moveInput = new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y).normalized;
        }
        if (context.canceled)
        {
            moveInput = Vector3.zero;
        }
    }

    public void LookMouse(InputAction.CallbackContext context)
    {
        if (GameController.isPaused) return;

        if (context.performed)
        {
            Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, rotateRaycastLayerMask))
            {
                transform.LookAt(
                    hit.point
                );
            }
        }
    }

    public void LookController(InputAction.CallbackContext context)
    {
        if (GameController.isPaused) return;

        if (context.performed)
        {
            transform.LookAt(
                transform.localPosition +
                new Vector3(context.ReadValue<Vector2>().x, 0f, context.ReadValue<Vector2>().y)
            );
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (GameController.isPaused) return;

        if (context.performed)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * meleeRange, meleeRange, Enemy.layerMask);
            if (hits.Length > 0)
            {
                MeleeAttack(hits);
            }
            else
            {
                RangedAttack();
            }
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameController.isPaused) GameController.Resume();
            else GameController.Pause();
        }
    }
    #endregion





    #region CLASS METHODS
    private void MeleeAttack(Collider[] hits)
    {
        if (currentMeleeCooldown <= 0f)
        {
            animator.SetTrigger("onMelee");

            currentMeleeCooldown = meleeCooldown;
        }
    }

    public void MeleeAttackAnimationEvent()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * meleeRange, meleeRange, Enemy.layerMask);
        foreach (Collider hit in hits)
        {
            hit.attachedRigidbody.GetComponent<Enemy>()?.TakeDamage(1);
        }
    }

    private void RangedAttack()
    {
        if (currentRangedCooldown <= 0f)
        {
            animator.SetTrigger("onShoot");

            var bullet = bulletPool.GetNext();
            bullet.transform.SetPositionAndRotation(bulletSpawnPoint.position, transform.rotation);
            bullet.SetActive(true);
            muzzleParticles.Play();

            currentRangedCooldown = rangedCooldown;
        }
    }

    public void TakeDamage(int damage)
    {
        if (invulnerable) return;

        currentHealth -= damage;
        UIController.UpdatePlayerHealth(currentHealth);
        if (currentHealth <= 0) { GameController.GameOver(); }
        else { StartCoroutine(TakeDamageCoroutine()); }
    }

    private IEnumerator TakeDamageCoroutine()
    {
        invulnerable = true;
        
        yield return StartCoroutine(flash.Flash(invulnerabilityTime, .1f));

        invulnerable = false;
    }
    #endregion
}