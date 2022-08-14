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
    [Range(0, 20)][SerializeField] private float movementSpeed = 8f;
    [Range(0, 15)][SerializeField] private float jumpSpeed = 5f;
    private PlayerAttack playerAttack;
    private CapsuleCollider2D headCollider;
    //get collider for head

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        flipPlayer = GetComponent<FlipPlayer>();
        movementInput = GetComponent<IMovementInput>();
        checkGround = GetComponent<CheckGround>();
        playerAttack = GetComponent<PlayerAttack>();
        headCollider = GetComponent<CapsuleCollider2D>();
        //sbscript a onfire event to the player attack script and play attack animation
        movementInput.OnFireEvent += playerAttack.Attack;
        playerAttack.OnFireEventAnimation += playerAnimations.PlayerAttackAnimation;
    }

    void Update()
    {
        Jump();
        Move();
        Couch();
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
    private void Couch()
    {
        headCollider.enabled = !movementInput.IsCrouching;
        playerAnimations.PlayerCrouch(movementInput.IsCrouching);
        playerMovement.SetCrouch(movementInput.IsCrouching);
    }
}
