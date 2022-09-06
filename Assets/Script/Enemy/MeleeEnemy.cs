using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float colliderDistance;
    float nextAttack = float.MaxValue;
    [SerializeField] private LayerMask whatIsPlayer;
    public BoxCollider2D boxCollider;
    private Animator animator;
    private Health playerHealth;
    private Health enemyHealth;
    private EnemyPatrol enemyPatrol;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        enemyHealth = GetComponentInParent<Health>();
    }
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        if (enemyPatrol != null && enemyHealth.CurrentHealth <= 0)
            enemyPatrol.enabled = false;

        nextAttack += Time.deltaTime;
        //Attack only when player is in range
        if (PlayerInSight() && playerHealth.CurrentHealth >= 0)
        {
            Debug.Log(" player in sight");
            if (nextAttack >= attackCooldown)
            {   
                nextAttack = 0;
                animator.SetTrigger("attack");
            }
        }
        if (enemyPatrol != null && enemyHealth.CurrentHealth >= 1)
            enemyPatrol.enabled = !PlayerInSight();

    }
    private bool PlayerInSight()
    {
        //Check if player is in sight
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * -transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0f, Vector2.left, 0, whatIsPlayer);
        if (hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>();
        }
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * -transform.localScale.x * colliderDistance,
         new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        //Damage player
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }

}
