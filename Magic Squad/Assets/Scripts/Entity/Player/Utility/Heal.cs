using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : AbilityBase
{
    [Header("Stats")]
    [SerializeField] private float soulCost = 10f;
    [SerializeField] private float healAmount = 10f;
    [SerializeField] private float healCooldown = .3f;
    private float _lastHeal;

    [Header("Input")] 
    [SerializeField] private KeyCode healKey = KeyCode.Q;

    private PlayerBase _playerBase;
    
    protected override float SoulCost { get; set; }

    protected override void InitializeAbstractedStats()
    {
        SoulCost = soulCost;
    }

    protected override bool DoAbility()
    {
        // Checks if we are already full health so we dont waste soul
        if (_playerBase.GetCurrentHealth() >= _playerBase.GetMaxHealth())
            return false;
        
        // This will do the checks for decreasing soul amount. If we dont have enough soul we will exit and not start heal.
        if (!base.DoAbility())
            return false;

        DoHeal();
        return true;
    }

    private void Start()
    {
        InitializeAbstractedStats();
        _playerBase = GetComponent<PlayerBase>();
    }

    private void Update()
    {
        CheckHeal();
    }

    private void DoHeal()
    {
        _playerBase.AddHealth(healAmount);
        _lastHeal = Time.time;
    }
    
    private void CheckHeal()
    {
        // if you are pressing heal key and its passed the cooldown
        if (Input.GetKeyDown(healKey) && Time.time - _lastHeal > healCooldown)
        {
            DoAbility();
        }
    }
}
