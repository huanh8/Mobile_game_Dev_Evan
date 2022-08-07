using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMovementInput
{
    Vector2 MovementInputVector { get; }
    float Horizontal { get; }
    bool IsJumping { get; }

    event Action OnFireEvent;
}
