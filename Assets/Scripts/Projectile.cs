using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float disableTime;
    private float currentDisableTime;

    [Header("References")]
    [SerializeField] private Rigidbody rb;

    private Vector3 nextPosition;
    private Vector3 direction;
    private float distance;
    private int layerMask = 1 << 8 | 1 << 10 | 1 << 11;
    private RaycastHit hit;





    private void OnEnable()
    {
        currentDisableTime = 0f;
    }

    private void FixedUpdate()
    {
        nextPosition = transform.position + (transform.forward * moveSpeed * Time.fixedDeltaTime);
        direction = nextPosition - transform.position;
        distance = Vector3.Distance(nextPosition, transform.position);

        // Raycast the bullet forward to check for collisions manually (had better results than Rigidbody)
        if (Physics.Raycast(new Vector3(transform.position.x, 0f, transform.position.z), direction, out hit, distance, layerMask))
        {
            if (hit.collider.gameObject.layer == Enemy.layer) hit.collider.attachedRigidbody.GetComponent<Enemy>()?.TakeDamage(damage);
            else if (hit.collider.gameObject.layer == EnemyTank.shieldLayer) hit.collider.attachedRigidbody.GetComponent<EnemyTank>()?.ShieldDamaged();

            gameObject.SetActive(false);
        }
        else transform.position = nextPosition;
    }

    private void LateUpdate()
    {
        if (currentDisableTime >= disableTime) gameObject.SetActive(false);
        else currentDisableTime += Time.fixedDeltaTime;
    }
}