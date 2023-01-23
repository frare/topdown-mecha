using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    public static readonly int shieldLayer = 11;

    [Space(15)]
    [Header("TANK PROPERTIES")]
    [SerializeField] protected Transform body;
    [SerializeField] protected Transform shield;
    [SerializeField] protected float shieldRotationSpeed;

    private Vector3 directionToPlayer;
    private Coroutine shieldDamagedCoroutine;





    protected override void FixedUpdate()
    {
        directionToPlayer = (Player.position - transform.position);

        navMeshAgent.destination = Player.position;
        // rb.MovePosition(transform.position + vectorToPlayer.normalized * currentMoveSpeed * Time.deltaTime);
        body.LookAt(directionToPlayer);

        var targetAngle = Vector3.SignedAngle(transform.forward, directionToPlayer, Vector3.up);
        shield.localEulerAngles = new Vector3(0f, Mathf.MoveTowardsAngle(shield.localEulerAngles.y, targetAngle, shieldRotationSpeed * 360 * Time.deltaTime), 0f);
    }

    public void ShieldDamaged()
    {
        if (shieldDamagedCoroutine != null) StopCoroutine(shieldDamagedCoroutine);
        shieldDamagedCoroutine = StartCoroutine(ShieldDamagedCoroutine());
    }

    private IEnumerator ShieldDamagedCoroutine()
    {
        var time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            shield.localScale = Vector3.Lerp(Vector3.one * 1.5f, Vector3.one, time);

            yield return null;
        }
        shield.localScale = Vector3.one;
    }
}