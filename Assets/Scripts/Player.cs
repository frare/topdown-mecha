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

    [Header("References")]
    [SerializeField] private Animator animator;
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
    }

    private void Update()
    {
        transform.localPosition += moveInput * moveSpeed * Time.deltaTime;

        currentRangedCooldown += Time.deltaTime;
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

    public void MelleeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            MeleeAtack();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // if melee

            // else if shooting
            RangedAttack();
        }
    }
    #endregion





    #region CLASS METHODS
    private void MeleeAtack()
    {
        animator.SetTrigger("onAttack");
    }

    private void RangedAttack()
    {
        if (currentRangedCooldown >= rangedCooldown)
        {
            GameObject bullet = bulletPool.GetNext();
            bullet.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.SetActive(true);
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