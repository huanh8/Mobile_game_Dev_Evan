using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WhatInSight
{
    public static GameObject EnemyInSight(Transform transform, BoxCollider2D boxCollider, float sightRange, float colliderDistance, LayerMask whatIsEnemies)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * sightRange * -transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y * sightRange,
            boxCollider.bounds.size.z),
            0f, Vector2.left, 0, whatIsEnemies);
        return hit.collider.gameObject;
    }
    public static void OnDrawGizmos(Transform transform, BoxCollider2D boxCollider, float sightRange, float colliderDistance, LayerMask whatIsEnemies)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * sightRange * -transform.localScale.x * colliderDistance,
         new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y * sightRange, boxCollider.bounds.size.z));
    }
}
