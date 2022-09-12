using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeEnemy : Enemy
{
    public override void AttckType()
    {
        DamagePlayer();
    }
}
