using UnityEngine;

public class Behaviour_ResetTrailOnEnable : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    private void OnEnable() => trail.Recicle();
}
