using System.Collections;
using UnityEngine;

public class Player_AnimatorControl : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GetMoveDirection moveDirection;
    [SerializeField] private Animator animator;

    [Header("Variables")]
    [SerializeField] private float XMult;
    [SerializeField] private float YMult;
    [Space]
    [SerializeField] private float velXLerp;
    [SerializeField] private float velYLerp;

    private float currentVelX;
    private float currentVelY;

    private IEnumerator Start()
    {
        moveDirection.enabled = false;
        yield return new WaitForEndOfFrame();
        moveDirection.enabled = true;
    }

    private void Update()
    {
        var crossRig = moveDirection.DotProductRig;
        var crossFwd = moveDirection.DotProductFwd;

        currentVelX = Mathf.Lerp(currentVelX, Mathf.Clamp(crossRig * XMult, -1, 1), Time.deltaTime * velXLerp);
        currentVelY = Mathf.Lerp(currentVelY, Mathf.Clamp(crossFwd * YMult, -1, 1), Time.deltaTime * velYLerp);

        animator.SetFloat("VelX", currentVelX);
        animator.SetFloat("VelY", currentVelY);
    }
}