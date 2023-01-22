using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    [Space(15)]
    [Header("TANK PROPERTIES")]
    [SerializeField] protected Transform body;
    [SerializeField] protected Transform shield;
    [SerializeField] protected float shieldRotationSpeed;
    private Vector3 directionToPlayer;





    protected override void FixedUpdate()
    {
        directionToPlayer = (Player.position - transform.position);

        navMeshAgent.destination = Player.position;
        // rb.MovePosition(transform.position + vectorToPlayer.normalized * currentMoveSpeed * Time.deltaTime);
        body.LookAt(directionToPlayer);

        var targetAngle = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);
        shield.localEulerAngles = new Vector3(0f, Mathf.MoveTowardsAngle(shield.localEulerAngles.y, targetAngle, shieldRotationSpeed * 360 * Time.deltaTime), 0f);
    }
}