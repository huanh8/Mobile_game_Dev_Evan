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
    [Range(0, 20)][SerializeField] private float movementSpeed = 8f;
    [Range(0, 20)][SerializeField] private float dashSpeed = 5f;
    [Range(0, 15)][SerializeField] private float jumpSpeed = 5f;
    private PlayerAttack playerAttack;

    //get collider for head

    [Range(0, 1)][SerializeField] private float dashTime = 0.5f;
    float nextDash = 0;

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
    private void Crouch()
    {
        playerAnimations.PlayerCrouchAnimation(movementInput.IsCrouching);
    }
    private void Dash()
    {
        if (Time.time >= nextDash)
        {
            playerAnimations.PlayerDashAnimation();
            StartCoroutine(playerMovement.DashPlayer(movementInput.MovementInputVector, dashSpeed, dashTime));
            nextDash = Time.time + dashTime;
        }
    }

}
