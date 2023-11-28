using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    // Projectile Stats
    private float _damage;
    private float _speed;
    private float _range;
    
    // Physics
    private Vector3 _direction;

    public void ProjectileInitialize(Dictionary<ShootingEnemy.Stats, float> stats, Vector3 direction)
    {
        _damage = stats[ShootingEnemy.Stats.Damage];
        _speed = stats[ShootingEnemy.Stats.Speed];
        _range = stats[ShootingEnemy.Stats.Range];
        _direction = direction;
        
        ProjectileMove();
    }

    private void ProjectileMove()
    {
        rb.AddForce(_direction * _speed, ForceMode.Impulse);
    }
}
