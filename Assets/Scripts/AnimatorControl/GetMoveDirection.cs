using UnityEngine;

public class GetMoveDirection : MonoBehaviour
{
    private Vector3 movement = Vector3.zero;
    private Vector3 prevpos = Vector3.zero;
    private Vector3 newpos = Vector3.zero;

    private Vector3 fwd = Vector3.zero;
    private Vector3 rig = Vector3.zero;

    public float DotProductFwd { get; private set; }
    public float DotProductRig { get; private set; }

    public bool IsMovingForward { get; private set; }
    public bool IsMovingRight { get; private set; }

    public bool IsMoving { get; private set; }

    private void Update()
    {
        newpos = transform.position;
        movement = (newpos - prevpos);

        DotProductFwd = Vector3.Dot(fwd, movement) / Time.deltaTime;
        IsMovingForward = DotProductFwd > 0;

        DotProductRig = Vector3.Dot(rig, movement) / Time.deltaTime;
        IsMovingRight = DotProductRig > 0;

        IsMoving = Mathf.Abs(DotProductFwd) > 0 || Mathf.Abs(DotProductRig) > 0;
    }

    private void LateUpdate()
    {
        prevpos = transform.position;

        fwd = transform.forward;
        rig = transform.right;
    }
}