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
        animator.SetBool("isMoving", false);
    }
    void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-speed);
            else
                ChangeDirection();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(speed);
            else
                ChangeDirection();
        }

    }
    private void MoveInDirection(float _direction)
    {
        animator.SetBool("isMoving", true);
        enemy.transform.Translate(transform.right * _direction * Time.deltaTime);
        flipEnemy.Filp(-_direction);
    }

    void ChangeDirection()
    {
        animator.SetBool("isMoving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            idleTimer = 0;
            movingLeft = !movingLeft;
        }
    }
}
