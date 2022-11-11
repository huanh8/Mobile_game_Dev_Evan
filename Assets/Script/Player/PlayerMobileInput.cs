using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMobileInput : MonoBehaviour, IMovementInput
{
    public Vector2 MovementInputVector { get; private set; }
    public float Horizontal { get; private set; }
    public bool IsCrouching { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsBlocking { get; private set; }
    public event Action OnFireEvent;
    public event Action OnDashEvent;

    void Update()
    {
        GetCouchInput();
        GetJumpInput();
        GetDashInput();
        GetMovementInput();
        GetFireInput();
        GetBlockInput();
    }

    private void GetMovementInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        MovementInputVector = new Vector2(Horizontal, 0);
    }
    private void GetJumpInput()
    {
        IsJumping = Input.GetButtonDown("Jump");
    }

    private void GetFireInput()
    {
        if (Input.GetButtonDown("Fire1") && !IsBlocking && !IsCrouching)
        {
            OnFireEvent?.Invoke();
        }
    }
    private void GetCouchInput()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            IsCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            IsCrouching = false;
        }
    }
    private void GetDashInput()
    {
        if (IsCrouching && IsJumping)
        {
            OnDashEvent?.Invoke();
        }
    }
    private void GetBlockInput()
    {
        if (IsCrouching && Input.GetButtonDown("Fire1"))
        {
            IsBlocking = true;
        }
        else if (!IsCrouching || Input.GetButtonUp("Fire1"))
        {
            IsBlocking = false;
        }
    }
}
