using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeeker : Enemy
{
    [Space(15)]
    [Header("SEEKER PROPERTIES")]
    [SerializeField] private float distanceForAttackAnimation;






    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var attackAnimation = Vector3.Distance(transform.position, Player.position) < distanceForAttackAnimation;
        animator.SetBool("isAttacking", attackAnimation);
    }
}