using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] protected float attackCooldown = 1.5f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float attackRange = 1f;
    protected float nextAttack = float.MaxValue;

    [Header("Collider Parameters")]
    [SerializeField] protected float colliderDistance = 1f;
    public BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] protected LayerMask whatIsPlayer;

    protected Animator animator;
    protected Health playerHealth;
    protected Health enemyHealth;
    protected EnemyPatrol enemyPatrol;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        enemyHealth = GetComponent<Health>();
    }

    void Update()
    {
        Attack();
    }
    protected void Attack()
    {
        if (enemyPatrol != null && enemyHealth.CurrentHealth <= 0)
            enemyPatrol.enabled = false;

        nextAttack += Time.deltaTime;
        // Attack only when player is in range
        if (EnemyInSight() && playerHealth.CurrentHealth > 0)
        {
            if (nextAttack >= attackCooldown)
            {   //play attack animation
                nextAttack = 0;
                animator.SetTrigger("Attack");
            }
        }
        if (enemyPatrol != null && enemyHealth.CurrentHealth >= 1)
            enemyPatrol.enabled = !EnemyInSight();
    }
    protected bool EnemyInSight()
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
    protected void OnDrawGizmos()
    {
        if (boxCollider == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * -transform.localScale.x * colliderDistance,
         new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    protected virtual void DamagePlayer()
    {
        //Damage player
        if (playerHealth != null)
            playerHealth.TakeDamage(damage, transform.gameObject);

    }
    protected void OnCollisionEnter2D(Collision2D hitInfor)
    {
        if (((1 << hitInfor.gameObject.layer) & whatIsPlayer) != 0)// check player layer => touch and damage player
        {
            Health h = hitInfor.gameObject.GetComponent<Health>();
            if (h == null || h.IsDead) return;
            h.TakeDamage(damage, transform.gameObject, true);
        }
    }
    public void IsHurtAnimation()
    {
        animator.SetTrigger("IsHurt");
    }
    public void IsDeadAnimation()
    {
        animator.SetBool("IsDead", true);
    }
}
