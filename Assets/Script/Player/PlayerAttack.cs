using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private Transform attackPoint;
    [Range(0, 1)][SerializeField] private float attackRange = 0.8f;
    public LayerMask whatIsEnemies;
    [Range(0, 1)][SerializeField] private float attacCooldown = 0.5f;
    float nextAttack = 0;
    public GameObject attackEffect;
    [SerializeField] private int attackDamage = 20;
    public List<GameObject> projectiles;
    public BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance = 1f;
    [SerializeField] private float sightRange = 1f;

    void Start()
    {
        attackPoint = transform.GetChild(0);
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Attack()
    {
        if (Time.time >= nextAttack)
        {   //play attack animation

            if (!EnemyInSight())
                animator.SetTrigger("Shooting");
            else
                animator.SetTrigger("Attack");


            //playerAnimations.PlayerAttackAnimation();
            nextAttack = Time.time + attacCooldown;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemies);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().TakeDamage(attackDamage);
                Debug.Log("Attack enemy");
                GameObject effect = Instantiate(attackEffect, enemy.transform.position, Quaternion.identity);
                Destroy(effect, 0.5f);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void ShootFireBall()
    {
        float direction = transform.localScale.x;
        projectiles[FindFireBall()].transform.position = attackPoint.position;
        projectiles[FindFireBall()].GetComponent<Projectile>().SetDirection(direction);
    }
    private int FindFireBall()
    {
        for (int i = 0; i < projectiles.Count; i++)
            if (!projectiles[i].activeInHierarchy)
                return i;
        return 0;
    }

    private bool EnemyInSight()
    {
        //Check if player is in sight
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * sightRange * -transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y * sightRange, 
            boxCollider.bounds.size.z),
            0f, Vector2.left, 0, whatIsEnemies);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * sightRange * -transform.localScale.x * colliderDistance,
         new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y * sightRange, boxCollider.bounds.size.z));
    }
}
