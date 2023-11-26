using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    [SerializeField]
    private float maxSoul = 100;

    [SerializeField]
    private float minSoul = 0;

    [SerializeField]
    private float currentSoul = 0;

    // Returns a bool stating weather or not it was able to add the full amount
    public bool AddSoul(float amount)
    {
        if (amount + currentSoul > maxSoul)
        {
            currentSoul = maxSoul;
            return false;
        }
        
        currentSoul += amount;
        return true;
    }

    // Returns a bool stating weather or not it can subtract the soul. (Doesn't subtract the soul if there isn't enough cause then cant do ability)
    public bool SubtractSoul(float amount)
    {
        if (currentSoul - amount <= minSoul)
        {
            return false;
        }
        
        currentSoul -= amount;
        return true;
    }

    public float GetMaxSoul()
    {
        return maxSoul;
    }

    public float GetMinSoul()
    {
        return minSoul;
    }

    public float GetCurrentSoul()
    {
        return currentSoul;
    }
}

