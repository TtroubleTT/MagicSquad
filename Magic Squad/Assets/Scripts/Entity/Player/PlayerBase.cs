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
        bool addedHealth = base.AddHealth(amount);
        _barImage.fillAmount = CurrentHealth / MaxHealth;
        return addedHealth;
    }
    
    // Returns a bool of weather or not the entity is alive
    public override bool SubtractHealth(float amount)
    {
        bool subtractedHealth = base.SubtractHealth(amount);
        _barImage.fillAmount = CurrentHealth / MaxHealth;
        return subtractedHealth;
    }
}
