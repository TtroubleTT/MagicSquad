using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : EntityBase
{
    protected override float MaxHealth { get; } = 100;
    protected override float CurrentHealth { get; set; } = 100;

    protected override void Die()
    {
        // what to do when a player dies
    }
    
    public override bool AddHealth(float amount)
    {
        if (CurrentHealth + amount > MaxHealth)
        {
            CurrentHealth = MaxHealth;
            return false;
        }

        CurrentHealth += amount;
        return true;
    }
    
    // Returns a bool of weather or not the entity is alive
    public override bool SubtractHealth(float amount)
    {
        if (CurrentHealth - amount <= 0)
        {
            CurrentHealth = 0;
            Debug.Log("die");
            return false;
        }
        
        CurrentHealth -= amount;
        Debug.Log($"health: {CurrentHealth}");
        return true;
    }
}
