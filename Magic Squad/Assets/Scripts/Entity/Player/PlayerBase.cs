using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBase : EntityBase
{
    protected override float MaxHealth { get; } = 100;
    protected override float CurrentHealth { get; set; } = 100;

    private Image _barImage; 
    protected override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Awake()
    {
        _barImage = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        _barImage.fillAmount = CurrentHealth / MaxHealth;
    }

    public override bool AddHealth(float amount)
    {
        if (CurrentHealth + amount > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            _barImage.fillAmount = CurrentHealth / MaxHealth;
            return false;
        }

        CurrentHealth += amount;
        _barImage.fillAmount = CurrentHealth / MaxHealth;
        return true;
    }
    
    // Returns a bool of weather or not the entity is alive
    public override bool SubtractHealth(float amount)
    {
        if (CurrentHealth - amount <= 0)
        {
            CurrentHealth = 0;
            _barImage.fillAmount = CurrentHealth / MaxHealth;
            Die();
            return false;
        }
        
        CurrentHealth -= amount;
        _barImage.fillAmount = CurrentHealth / MaxHealth;
        return true;
    }
}
