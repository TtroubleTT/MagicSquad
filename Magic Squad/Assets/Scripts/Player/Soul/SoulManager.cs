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

    public void AddSoul(float amount)
    {
        if (amount + currentSoul > maxSoul)
        {
            currentSoul = maxSoul;
        }
        else
        {
            currentSoul = currentSoul + amount;
        }
    }

    public float GetMaxSoul()
    {
        return maxSoul;
    }

    public float GetCurrentSoul()
    {
        return currentSoul;
    }
}

