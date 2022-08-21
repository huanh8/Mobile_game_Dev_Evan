using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAnimations playerAnimations;
    private FlipPlayer flipPlayer;
    private IMovementInput movementInput;//Interface for mobile input
    private CheckGround checkGround;

    private PlayerAttack playerAttack;

    //get collider for head

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        flipPlayer = GetComponent<FlipPlayer>();
        movementInput = GetComponent<IMovementInput>();
        checkGround = GetComponent<CheckGround>();
        playerAttack = GetComponent<PlayerAttack>();

        //sbscript a onfire event to the player attack script and play attack animation
        movementInput.OnFireEvent += playerAttack.Attack;
        playerAttack.OnFireEventAnimation += playerAnimations.PlayerAttackAnimation;
        movementInput.OnDashEvent += Dash;
    }

    void Update()
    {
        Jump();
        Move();
        Crouch();
    }
    private void Move()
    {   //play walk animation
        playerAnimations.PlayWalkAnimation(movementInput.Horizontal);
        //movePlayer
        playerMovement.MovePlayer(movementInput.MovementInputVector);
        //flip player
        flipPlayer.Filp(movementInput.Horizontal);
    }

    private void Jump()
    {
        playerAnimations.PlayerJumpAnimation(!checkGround.IsGrounded());

        if (checkGround.IsGrounded() && movementInput.IsJumping)
        {
            playerMovement.PlayerJump();
        }
    }
    private void Crouch()
    {
        playerAnimations.PlayerCrouchAnimation(movementInput.IsCrouching);
    }
    private void Dash()
    {
        playerMovement.PlayerCanDash(movementInput.MovementInputVector);
        playerAnimations.PlayerDashAnimation(playerMovement.IsDashing);
    }

}
