using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    public void MeleeAttackDamageEvent()
    {
        Player.instance.MeleeAttackAnimationEvent();
    }
}