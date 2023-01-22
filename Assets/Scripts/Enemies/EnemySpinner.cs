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
        navMeshAgent.destination = Player.position;
        // rb.MovePosition(transform.position + (Player.position - transform.position).normalized * currentMoveSpeed * Time.deltaTime);

        var distanceToPlayer = Vector2.Distance(transform.position, Player.position);
        currentMoveSpeed = distanceToPlayer <= speedUpDistance ? moveSpeed * speedUpModifier : moveSpeed;

        var attackAnimation = Vector3.Distance(transform.position, Player.position) < speedUpDistance;
        animator.SetBool("isAttacking", attackAnimation);
    }
}
