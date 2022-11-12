using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class PlayerMobileInput : MonoBehaviour, IMovementInput
{
    public Vector2 MovementInputVector { get; private set; }
    public float Horizontal { get; private set; }
    public bool IsCrouching { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsBlocking { get; private set; }
    public event Action OnFireEvent;
    public event Action OnDashEvent;
    public Joystick joystick;
    public MobileButton jumpButton;
    public MobileButton attackButton;
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
        Horizontal = joystick.Horizontal;
        if (joystick.Horizontal >= 0.2f)
            Horizontal = 1;
        else if (joystick.Horizontal <= -0.2f)
            Horizontal = -1;
        else
            Horizontal = 0;
        MovementInputVector = new Vector2(Horizontal, 0);
    }
    private void GetJumpInput()
    {
        IsJumping = joystick.Vertical >= 0.5f || jumpButton.IsPressed;
    }


    private void GetFireInput()
    {
        if (attackButton.IsPressed && !IsBlocking && !IsCrouching)
        {
            OnFireEvent?.Invoke();
        }
    }
    private void GetCouchInput()
    {
        if (joystick.Vertical <= -0.5f)
        {
            IsCrouching = true;
        }
        else
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
        if (IsCrouching && attackButton.IsPressed)
        {
            IsBlocking = true;
        }
        else if (!IsCrouching || !attackButton.IsPressed)
        {
            IsBlocking = false;
        }
    }
}
