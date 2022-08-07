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

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        flipPlayer = GetComponent<FlipPlayer>();
        movementInput = GetComponent<IMovementInput>();
        checkGround = GetComponent<CheckGround>();
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
        flipPlayer.Filp(movementInput.Horizontal > 0);
    }

    private void Jump()
    {
        playerAnimations.PlayerJumpAnimation(!checkGround.IsGrounded());

        if (checkGround.IsGrounded() && movementInput.IsJumping)
            playerMovement.PlayerJump(jumpSpeed);
            
    }

}
