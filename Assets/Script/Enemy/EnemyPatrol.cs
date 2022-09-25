using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform leftEdge;
    public Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform enemy;
    private bool movingLeft = true;
    private FlipPlayer flipEnemy;
    private Animator animator;
    private Health enemyHealth;

    [Header("Enemy Idle")]
    [SerializeField] private float idleDuration = 1;
    private float idleTimer = 0;

    void Start()
    {
        animator = enemy.GetComponent<Animator>();
        flipEnemy = enemy.GetComponent<FlipPlayer>();
        enemyHealth = enemy.GetComponent<Health>();
    }
    void OnDisable()
    {
        animator.SetBool("IsMoving", false);
    }
    void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                Vector2 direction = (leftEdge.position - enemy.position).normalized;
                MoveInDirection(direction);
            }

            else
                ChangeDirection();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                Vector2 direction = (rightEdge.position - enemy.position).normalized;
                MoveInDirection(direction);
            }

            else
                ChangeDirection();
        }

    }
    private void MoveInDirection(Vector2 direction)
    {
        animator.SetBool("IsMoving", true);
        enemy.transform.Translate(direction * speed * Time.deltaTime);
        flipEnemy.Filp(-direction.x);
    }

    void ChangeDirection()
    {
        animator.SetBool("IsMoving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            idleTimer = 0;
            movingLeft = !movingLeft;
        }
    }

}
