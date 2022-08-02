using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMovementInput 
{
    Vector2 MovementInputVector { get; }

    event Action OnFireEvent;
}
