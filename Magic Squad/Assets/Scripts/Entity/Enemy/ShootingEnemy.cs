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

    // Checks if the distance between player and enemy is within the range they are allowed to fire
    private bool IsInRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance <= range)
            return true;

        return false;
    }

    // Checks if the player is within the enemies line of sight
    private bool InLineOfSight()
    {
        if (Physics.Raycast(transform.position, (_player.transform.position - transform.position), out RaycastHit hitInfo, range))
        {
            if (hitInfo.transform.gameObject == _player)
            {
                return true;
            }
        }

        return false;
    }

    // If the shot cooldown has passed, and the player is within shooting range, and line of sight then shoot
    private void CheckShoot()
    {
        if (Time.time - _lastShotTime > shotCooldown && IsInRange() && InLineOfSight())
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