using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBlast : AbilityBase, ICombat
{
    public float Damage { get; set; }
    
    protected override float SoulCost { get; set; }

    [Header("References")] 
    [SerializeField] private Transform camTrans;
    [SerializeField] private GameObject projectilePrefab;
    
    [Header("Input")] 
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse1;

    [Header("Stats")]
    [SerializeField] private float soulCost = 20;
    [SerializeField] private float attackCooldown = .5f;
    private float _lastAttack;
    
    [Header("Projectile Stats")] 
    [SerializeField] private float damage = 40f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float range = 120f;
    
    private readonly Dictionary<ShootingEnemy.Stats, float> _projectileStats = new();

    private AudioManager _audioManager;

    protected override void InitializeAbstractedStats()
    {
        Damage = damage;
        SoulCost = soulCost;
    }
    
    private void InitializeStats()
    {
        _projectileStats.Add(ShootingEnemy.Stats.Damage, Damage);
        _projectileStats.Add(ShootingEnemy.Stats.Speed, speed);
        _projectileStats.Add(ShootingEnemy.Stats.Range, range);
    }

    protected override bool DoAbility()
    {
        // This will do the checks for decreasing soul amount. If we dont have enough soul we will exit and not start heal.
        if (!base.DoAbility())
            return false;

        _lastAttack = Time.time;
        Attack();
        return true;
    }

    private void Start()
    {
        InitializeAbstractedStats();
        InitializeStats();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        CheckAttack();
    }

    private void CheckAttack()
    {
        // if you are pressing attack key and its passed the cooldown
        if (Input.GetKeyDown(attackKey) && Time.time - _lastAttack > attackCooldown)
        {
            DoAbility();
        }
    }

    public void Attack()
    {
        _audioManager.PlaySoundEffect(AudioManager.AudioType.Projectile);
        GameObject projectile = Instantiate(projectilePrefab, camTrans.position + (camTrans.forward * 2), camTrans.rotation);
        Vector3 direction = camTrans.forward.normalized; // Gets direction player is looking
        projectile.GetComponent<ShootingProjectile>().ProjectileInitialize(_projectileStats, direction);
    }
}
