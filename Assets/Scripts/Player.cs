using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Attributes")]
    [SerializeField] private int health;
    private int currentHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rangedCooldown;
    private float currentRangedCooldown;
    [SerializeField] private float meleeCooldown;
    private float currentMeleeCooldown;
    [SerializeField] private float meleeRange;
    [SerializeField] private float meleeSize;

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private ObjectPool bulletPool;

    private Vector3 moveInput;
    private Quaternion lookInput;
    private const int rotateRaycastLayerMask = 1 << 6;
    





    #region UNITY CALLBACKS
    private void Awake()
    {
        Player.instance = this;

        currentHealth = health;
        currentRangedCooldown = rangedCooldown;
        currentMeleeCooldown = meleeCooldown;
    }

    private void Update()
    {
        transform.localPosition += moveInput * moveSpeed * Time.deltaTime;

        currentRangedCooldown += Time.deltaTime;
        currentMeleeCooldown += Time.deltaTime;
    }
    #endregion





    #region INPUT SYSTEM ACTION CALLBACKS
    public void Move(InputAction.CallbackContext context)
    {
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
        if (context.performed)
        {   
            Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * meleeRange, meleeRange, Enemy.layerMask);
            if (hits.Length > 0)
            {
                MeleeAtack(hits);
            }
            else
            {
                RangedAttack();
            }
        }
    }
    #endregion





    #region CLASS METHODS
    private void MeleeAtack(Collider[] hits)
    {
        if (currentMeleeCooldown >= meleeCooldown)
        {
            // play melee attack animation
            foreach (Collider hit in hits)
            {
                hit.attachedRigidbody.GetComponent<Enemy>()?.TakeDamage(1);
            }

            currentMeleeCooldown = 0f;
        }
    }

    private void RangedAttack()
    {
        if (currentRangedCooldown >= rangedCooldown)
        {
            // play ranged attack animation
            GameObject bullet = bulletPool.GetNext();
            bullet.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.SetActive(true);

            currentRangedCooldown = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // if (currentHealth <= 0)
        // game over
        // else give feedback
    }
    #endregion
}