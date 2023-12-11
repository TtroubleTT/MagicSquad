using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class SoulManager : MonoBehaviour
{
    [SerializeField]
    private float maxSoul = 100;

    [SerializeField]
    private float minSoul = 0;

    [SerializeField]
    private float currentSoul = 100;
    
    private Image _barImage;

    private void Start()
    {
        _barImage = GameObject.FindGameObjectWithTag("SoulBar").GetComponent<Image>();
        _barImage.fillAmount = currentSoul / maxSoul;

    }
    // Returns a bool stating whether or not it was able to add the full amount
    public bool AddSoul(float amount)
    {
        if (currentSoul >= maxSoul)
        {
            return false;
        }
        
        if (amount + currentSoul > maxSoul)
        {
            currentSoul = maxSoul;
            _barImage.fillAmount = currentSoul/maxSoul;
            return true;
        }
        
        currentSoul += amount;
        _barImage.fillAmount = currentSoul / maxSoul;
        return true;
    }

    // Returns a bool stating weather or not it can subtract the soul. (Doesn't subtract the soul if there isn't enough cause then cant do ability)
    public bool SubtractSoul(float amount)
    {
        if (currentSoul - amount < minSoul)
        {
            return false;
        }

        currentSoul -= amount;
        _barImage.fillAmount = currentSoul / maxSoul;
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

