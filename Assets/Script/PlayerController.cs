using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimations playerAnimations;
    private FlipPlayer flipPlayer;
    private IMovementInput movementInput;//Interface for mobile input
    private CheckGround checkGround;
    private Transform attackPoint;
    [Range(0, 1)][SerializeField] private float attackRange = 0.5f;
    public LayerMask whatIsEnemies;
    [Range(0, 20)][SerializeField] private float movementSpeed = 8f;
    [Range(0, 15)][SerializeField] private float jumpSpeed = 5f;
    [Range(0, 1)][SerializeField] private float attackSpeed = 0.5f;
    float nextAttack = 0;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        flipPlayer = GetComponent<FlipPlayer>();
        movementInput = GetComponent<IMovementInput>();
        checkGround = GetComponent<CheckGround>();
        //get transform of attack point in child
        attackPoint = transform.GetChild(0);
    }
    void OnEnable()
    {
        movementInput.OnFireEvent += Attack;
    }
    void OnDisable()
    {
        movementInput.OnFireEvent -= Attack;
    }

    void Update()
    {
        Jump();
        Move();
    }
    private void Move()
    {   //play walk animation
        playerAnimations.PlayWalkAnimation(movementInput.Horizontal);
        //movePlayer
        playerMovement.MovePlayer(movementInput.MovementInputVector, movementSpeed);
        //flip player
        flipPlayer.Filp(movementInput.Horizontal);
    }

    private void Jump()
    {
        playerAnimations.PlayerJumpAnimation(!checkGround.IsGrounded());

        if (checkGround.IsGrounded() && movementInput.IsJumping)
        {
            playerMovement.PlayerJump(jumpSpeed);
        }
    }
    private void Attack()
    {
        if (Time.time >= nextAttack)
        {
            playerAnimations.PlayerAttackAnimation();
            nextAttack = Time.time + attackSpeed;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemies);
            foreach (Collider2D enemy in hitEnemies)
            {
                //enemy.GetComponent<Enemy>().TakeDamage(1);
                Debug.Log("Attack enemy");
            }
            Debug.Log("Attack");
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
