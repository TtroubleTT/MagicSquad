using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : EntityBase
{
    protected override float MaxHealth { get; } = 100;
    protected override float CurrentHealth { get; set; } = 100;

    protected override void Die()
    {
        // what to do when a player dies
    }
}
