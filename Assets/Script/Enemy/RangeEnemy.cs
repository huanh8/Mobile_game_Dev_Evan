using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    float nextAttack = float.MaxValue;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    public BoxCollider2D boxCollider;


    [Header("Player Layer")]
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Range Parameters")]
    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] private Transform firePoint;

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
        if (PlayerInSight())
        {
            Debug.Log(" player in sight");
            if (nextAttack >= attackCooldown)
            {   //play attack animation
                nextAttack = 0;
                animator.SetTrigger("attack");
                RangeAttack();
            }
        }
        if (enemyPatrol != null && enemyHealth.CurrentHealth >= 1)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private void RangeAttack()
    {
        float direction = transform.localScale.x;
        projectiles[FindFireBall()].transform.position = firePoint.position;
        projectiles[FindFireBall()].GetComponent<Projectile>().SetDirection(-direction);
    }
    private int FindFireBall()
    {
        for (int i = 0; i < projectiles.Count; i++)
            if (!projectiles[i].activeInHierarchy)
                return i;
        return 0;
    }

    private bool PlayerInSight()
    {
        //Check if player is in sight
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * -transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0f, Vector2.left, 0, whatIsPlayer);
        if (hit.collider != null)
            playerHealth = hit.collider.GetComponent<Health>();
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
