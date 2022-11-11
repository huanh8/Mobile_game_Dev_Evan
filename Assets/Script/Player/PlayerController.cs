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
    private Health playerHealth;
    public Transform blockPoint;
    private BoxCollider2D feetCollider;
    public GameObject HealthBar;
    //get collider for head

    void Awake()
    {
        feetCollider = GetComponent<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        flipPlayer = GetComponent<FlipPlayer>();
        movementInput = GetComponent<IMovementInput>();
        checkGround = GetComponent<CheckGround>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<Health>();
        blockPoint = transform.GetChild(1);
        //sbscript a onfire event to the player attack script and play attack animation
        //movementInput.OnFireEvent += playerAttack.Attack;
        // dash event
        //movementInput.OnDashEvent += Dash;
    }

    void Update()
    {
        checkeHealth();
        Jump();
        Move();
        Crouch();
        Blocking();
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
            AudioManager.instance.PlaySound(AudioManager.instance.JumpClip);
            playerMovement.PlayerJump();
        }

    }
    private void Crouch()
    {
        playerAnimations.PlayerCrouchAnimation(movementInput.IsCrouching);
        playerMovement.PlayerCrouch(movementInput.IsCrouching);
    }
    private void Dash()
    {
        if (checkGround.IsGrounded())
        {
            AudioManager.instance.PlaySound(AudioManager.instance.DashClip);
            playerMovement.PlayerCanDash(movementInput.MovementInputVector);
            playerAnimations.PlayerDashAnimation(playerMovement.IsDashing);
        }
    }
    private void checkeHealth()
    {
        enabled = playerHealth.CurrentHealth > 0;
    }

    private void Blocking()
    {

        if (movementInput.IsBlocking)
        {
            playerAnimations.PlayerBlockAnimation(true);
            playerMovement.MovePlayer(Vector2.zero);
            blockPoint.gameObject.SetActive(true);
            playerHealth.SetBlock(true);
        }
        else
        {
            playerAnimations.PlayerBlockAnimation(false);
            blockPoint.gameObject.SetActive(false);
            playerHealth.SetBlock(false);
        }
    }
    void OnDisable()
    {
        movementInput.OnFireEvent -= playerAttack.Attack;
        movementInput.OnDashEvent -= Dash;

        // set collider physics material to HasFriction
        if (feetCollider != null)
            feetCollider.sharedMaterial = new PhysicsMaterial2D("HasFriction");

        playerAnimations.PlayWalkAnimation(0f);
    }
    void OnEnable()
    {
        movementInput.OnFireEvent += playerAttack.Attack;
        movementInput.OnDashEvent += Dash;
        if (feetCollider != null)
            // set the physics material to NoFriction
            feetCollider.sharedMaterial = new PhysicsMaterial2D("NoFriction");
    }
    public void PlayerDeadEvent()
    {
        GameController.instance.GameOver();
        enabled = false;
    }
    public void PlayerWinEvent()
    {
        GameController.instance.GameWin();
        enabled = false;
    }

}
