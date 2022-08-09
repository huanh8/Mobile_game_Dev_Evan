using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour, IMovementInput
{
    public Vector2 MovementInputVector { get; private set; }
    public float Horizontal { get; private set; }
    public bool IsJumping { get; private set; }
    public event Action OnFireEvent;
    void Update()
    {
        GetMovementInput();
        GetJumpInput();
        GetFireInput();
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
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            OnFireEvent?.Invoke();
        }
    }
}
