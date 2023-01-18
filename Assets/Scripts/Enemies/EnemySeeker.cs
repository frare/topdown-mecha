using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeeker : Enemy
{
    [SerializeField] private float distanceForAnimation;




    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var attackAnimation = Vector3.Distance(transform.position, Player.GetPosition()) < distanceForAnimation;
        animator.SetBool("isAttacking", attackAnimation);
    }
}