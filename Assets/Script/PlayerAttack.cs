using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    private Transform attackPoint;
    [Range(0, 1)][SerializeField] private float attackRange = 0.8f;
    public LayerMask whatIsEnemies;
    [Range(0, 1)][SerializeField] private float attackSpeed = 0.5f;
    float nextAttack = 0;
    public event Action OnFireEventAnimation;
    void Start()
    {
        attackPoint = transform.GetChild(0);
    }

    public void Attack()
    {
        if (Time.time >= nextAttack)
        {   //play attack animation
            OnFireEventAnimation?.Invoke();
            //playerAnimations.PlayerAttackAnimation();
            nextAttack = Time.time + attackSpeed;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemies);
            foreach (Collider2D enemy in hitEnemies)
            {
                //enemy.GetComponent<Enemy>().TakeDamage(1);
                Debug.Log("Attack enemy");
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
