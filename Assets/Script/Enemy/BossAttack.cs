using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;
    public float attackRange = 0.5f;
    public LayerMask attckLayer;
    private bool isEnrange = false;
    private Health health;
    private Animator animator;
    public Transform attackPoint;
    [SerializeField] UnityEvent startTriggerEvent;
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
            startTriggerEvent.Invoke();
            animator.SetBool("IsEnraged", true);
        }
    }
    public void Attack()
    {
        // check if the boss is isEnrange
        int damage = isEnrange ? enragedAttackDamage : attackDamage;


        Collider2D hitEnemie = Physics2D.OverlapCircle(attackPoint.position, attackRange, attckLayer);
        if (hitEnemie != null)
        {
            hitEnemie.GetComponent<Health>().TakeDamage(damage, transform.gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void IsDead()
    {
        animator.SetBool("IsDead", true);
    }

}
