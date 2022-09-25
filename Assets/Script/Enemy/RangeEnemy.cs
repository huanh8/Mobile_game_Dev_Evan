using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeEnemy : Enemy
{
    [Header("Range Parameters")]
    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] private Transform firePoint;

    protected override void DamagePlayer()
    {
        float direction = transform.localScale.x;
        FireBalls.ShootFireBall(projectiles, -direction, firePoint.position, whatIsPlayer, gameObject.layer);
    }

}
