using UnityEngine;

public class GetRotationDirection : MonoBehaviour
{
    Vector3 oldForward;

    public bool IsRotating => Cross.magnitude > 0;
    public Vector3 Cross { get; private set; }

    void Start() => oldForward = transform.forward;
    void Update()
    {
        Cross = Vector3.Cross(oldForward, transform.forward) / Time.deltaTime;
        //crossY = cross.y;

        //if (cross.y > 0f)
        //{
        //    rotationDirection = RotationDirection.Rightward;
        //}
        //else if (cross.y < 0f)
        //{
        //    rotationDirection = RotationDirection.Leftward;
        //}
        //else
        //{
        //    rotationDirection = RotationDirection.NoRotation;
        //}

        oldForward = transform.forward;
    }
}