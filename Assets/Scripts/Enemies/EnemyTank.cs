using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    [Space(15)]
    [Header("TANK PROPERTIES")]
    [SerializeField] protected Transform body;
    [SerializeField] protected Transform shield;
    [SerializeField] protected float shieldSpeed;
    private Vector3 vectorToPlayer;





    protected override void FixedUpdate()
    {
        vectorToPlayer = (Player.position - transform.position);

        rb.MovePosition(transform.position + vectorToPlayer.normalized * currentMoveSpeed * Time.deltaTime);
        body.LookAt(vectorToPlayer);

        float targetAngle = Vector3.SignedAngle(transform.forward, vectorToPlayer, Vector3.up);
        shield.localEulerAngles = new Vector3(0f, Mathf.MoveTowardsAngle(shield.localEulerAngles.y, targetAngle, shieldSpeed * 360 * Time.deltaTime), 0f);
    }
}