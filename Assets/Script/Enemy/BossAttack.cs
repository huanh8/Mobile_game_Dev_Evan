using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;
    public Vector3 attackOffset;
    public float attackRange = 0.5f;
    public LayerMask attckLayer;
    private bool isEnrange = false;
    private Health health;
    private Animator animator;
    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (health.CurrentHealth <= health.maxHealth / 2)
        {
            isEnrange = true;
            animator.SetBool("IsEnraged", true);
        }
    }
    public void Attack()
    {
        // check if the boss is isEnrange
        int damage = isEnrange ? enragedAttackDamage : attackDamage;

        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D hitEnemie = Physics2D.OverlapCircle(pos, attackRange, attckLayer);
        if (hitEnemie != null)
        {
            hitEnemie.GetComponent<Health>().TakeDamage(damage, transform.gameObject);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }

}
