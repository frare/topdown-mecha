using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector3 moveInput;
    private Quaternion lookInput;





    #region UNITY CALLBACKS
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
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Ground")
                {
                    transform.LookAt(
                        hit.point
                    );
                }
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
            Debug.Log("Attack!");
        }
    }
    #endregion
}