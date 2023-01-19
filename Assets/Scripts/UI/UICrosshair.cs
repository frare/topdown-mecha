using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UICrosshair : MonoBehaviour, Controls.IUIActions
{
    [SerializeField] private Animator animator;
    [SerializeField] private float rotateSpeed;

    private Controls controls;






    private void Awake()
    {
        if (!Application.isEditor) Cursor.visible = false;
    }

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            controls.UI.SetCallbacks(this);
        }

        controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.UI.Disable();
    }

    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) 
        {
            animator.SetTrigger("onClick");
            animator.SetBool("clicked", true);
        }
        else if (ctx.canceled) animator.SetBool("clicked", false);
    }
}