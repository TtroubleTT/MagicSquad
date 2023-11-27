using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : EntityBase
{
    protected override float MaxHealth { get; } = 50;
    protected override float CurrentHealth { get; set; } = 50;

    protected override void Die()
    {
        // what to do when a player dies
    }
}
