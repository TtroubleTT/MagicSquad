using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EnemyBase : EntityBase
{
    protected virtual float SoulDropAmount { get; set; } = 10;

    private GameObject _soulObjectPrefab;

    protected override void Die()
    {
        SpawnSoul();
        Destroy(gameObject);
    }
    
    private void Awake()
    {
        _soulObjectPrefab = GameObject.FindGameObjectWithTag("SoulObject");
    }

    private void SpawnSoul()
    {
        Transform myTransform = transform;
        GameObject soul = Instantiate(_soulObjectPrefab, myTransform.position + myTransform.up, myTransform.rotation);
        soul.GetComponent<SoulObject>().InitializeSoulAmount(SoulDropAmount);
    }
}
