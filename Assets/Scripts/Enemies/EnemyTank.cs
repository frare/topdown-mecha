using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    [Space(15)]
    [Header("TANK PROPERTIES")]
    [SerializeField] private Transform shield;
    [SerializeField] private float shieldSpeed;





    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float targetAngle = Vector3.SignedAngle(transform.forward, Player.GetPosition() - transform.position, Vector3.up);
        shield.localEulerAngles = new Vector3(0f, Mathf.MoveTowardsAngle(shield.localEulerAngles.y, targetAngle, shieldSpeed * 360 * Time.deltaTime), 0f);
    }
}