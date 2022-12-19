using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpinner : Enemy
{
    [Space(15)]
    [Header("SPINNER PROPERTIES")]
    [SerializeField] private float speedUpDistance;
    [SerializeField] private float speedUpModifier;





    protected override void FixedUpdate()
    {
        rb.MovePosition(transform.position + (Player.GetPosition() - transform.position).normalized * currentMoveSpeed * Time.deltaTime);

        float distanceToPlayer = Vector3.Distance(transform.position, Player.GetPosition());
        currentMoveSpeed = distanceToPlayer <= speedUpDistance ? moveSpeed * speedUpModifier : moveSpeed;
    }
}
