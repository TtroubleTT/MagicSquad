using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyBase
{
    protected override float MaxHealth { get; set; }
    
    protected override float CurrentHealth { get; set; }

    [Header("Enemy Stats")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth = 50f;
    [SerializeField] private float soulAmount = 20f;
    [SerializeField] private float shotRange = 30f;
    [SerializeField] private float shotCooldown = 3f;
    private float _lastShotTime;

    [Header("Projectile Stats")] 
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 70f;
    
    [Header("References")] 
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform wandTransform;
    private GameObject _player;
    private Transform _playerTransform;
    private Animator _animator;
    
    // Projectile Stats
    public enum Stats
    {
        Damage = 0,
        Speed = 1,
        Range = 2,
    }
    
    // Eventually take stats stuff out of this class and into a class both player and enemy use
    private readonly Dictionary<Stats, float> _projectileStats = new();

    public override bool SubtractHealth(float amount)
    {
        bool stillAlive = base.SubtractHealth(amount);
        
        if (stillAlive)
            _animator.Play("GetHit");

        return stillAlive;
    }
    
    protected override void InitializeAbstractedStats()
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        SoulDropAmount = soulAmount;
    }

    private void InitializeStats()
    {
        _projectileStats.Add(Stats.Damage, damage);
        _projectileStats.Add(Stats.Speed, speed);
        _projectileStats.Add(Stats.Range, range);
    }

    private void Start()
    {
        InitializeStats();
        InitializeAbstractedStats();
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _playerTransform = _player.transform;
    }

    private void Update()
    {
        Vector3 playerPos = _playerTransform.position;
        Vector3 lookPoint = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        transform.LookAt(lookPoint);
        CheckShoot();
    }

    // Checks if the distance between player and enemy is within the range they are allowed to fire
    private bool IsInRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance <= shotRange)
            return true;

        return false;
    }

    // Checks if the player is within the enemies line of sight
    private bool InLineOfSight()
    {
        if (Physics.Raycast(transform.position, (_player.transform.position - transform.position), out RaycastHit hitInfo, shotRange))
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
        _animator.Play("Attack01");
        Transform myTransform = wandTransform;
        GameObject projectile = Instantiate(projectilePrefab, myTransform.position + (myTransform.forward * 2) + myTransform.up, myTransform.rotation);
        Vector3 direction = (_player.transform.position - transform.position).normalized; // Gets direction of player
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction);
    }
}
