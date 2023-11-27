using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : EntityBase
{
    protected override float MaxHealth { get; } = 50;
    protected override float CurrentHealth { get; set; } = 50;

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
