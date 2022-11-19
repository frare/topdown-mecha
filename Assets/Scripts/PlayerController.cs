using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed;

    [Header("References")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private ObjectPool bulletPool;

    private Vector3 moveInput;
    private Quaternion lookInput;
    private const int rotateRaycastLayerMask = 1 << 6;
    private float rangedCooldown = 0f;




    #region UNITY CALLBACKS
    private void Awake()
    {
        PlayerController.instance = this;
    }

    private void Update()
    {
        transform.localPosition += moveInput * moveSpeed * Time.deltaTime;
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
            // if melee

            // else if shooting
            RangedAttack();
        }
    }
    #endregion





    #region CLASS METHODS
    private void MeleeAtack()
    {

    }

    private void RangedAttack()
    {
        GameObject bullet = bulletPool.GetNext();
        bullet.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.SetActive(true);
    }
    #endregion
}