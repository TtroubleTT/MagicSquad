using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyBase
{
    [Header("Player Detection")] 
    [SerializeField] private float range = 30f;

    [Header("Shooting")] 
    [SerializeField] private float shotCooldown = 3f;
    private float _lastShotTime;
    
    // References
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CheckShoot();
    }

    private bool IsInRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance <= range)
            return true;

        return false;
    }

    private void CheckShoot()
    {
        if (Time.time - _lastShotTime > shotCooldown && IsInRange())
        {
            _lastShotTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("shoot");
    }
}
