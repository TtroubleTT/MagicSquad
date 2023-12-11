using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : EntityBase
{
    protected override float MaxHealth { get; } = 50;
    protected override float CurrentHealth { get; set; } = 50;

    protected virtual float SoulDropAmount { get; set; } = 20f;

    private GameObject _soulObjectPrefab;

    private void Awake()
    {
        _soulObjectPrefab = GameObject.FindGameObjectWithTag("SoulObject");
    }

    protected override void Die()
    {
        SpawnSoul();
        Destroy(gameObject);
    }

    private void SpawnSoul()
    {
        Transform myTransform = transform;
        GameObject soul = Instantiate(_soulObjectPrefab, myTransform.position, myTransform.rotation);
        soul.GetComponent<SoulObject>().InitializeSoulAmount(SoulDropAmount);
    }
}
