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
    public GameObject JoyStickPack;

    public bool canDash = false;
    public bool canBlock = false;

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
        if (checkGround.IsGrounded() && canDash)
        {
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

        if (movementInput.IsBlocking && canBlock)
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
        if (feetCollider != null)
            feetCollider.sharedMaterial = new PhysicsMaterial2D("HasFriction");

        playerAnimations.PlayWalkAnimation(0f);

        if (JoyStickPack != null)
            JoyStickPack.SetActive(false);

    }
    void OnEnable()
    {
        movementInput.OnFireEvent += playerAttack.Attack;
        movementInput.OnDashEvent += Dash;
        if (feetCollider != null)
            // set the physics material to NoFriction
            feetCollider.sharedMaterial = new PhysicsMaterial2D("NoFriction");
        if (JoyStickPack != null)
            JoyStickPack.SetActive(true);
        
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
    public void PlayerCanDash()
    {
        canDash = true;
    }
    public void PlayerCanBlock()
    {
        canBlock = true;
    }
    public void PlayerDashSound()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.DashClip);
    }
    public void PlayerJumpSound()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.JumpClip);
    }
    public void PlayerAttackSound()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.SwordClip);
    }
    public void PlayerWalkSound()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.WalkClip);
    }
    public void PlayerHurtSound()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.HurtClip);
    }
}
