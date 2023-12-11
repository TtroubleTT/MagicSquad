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
    private float currentSoul = 50;
    
    private Image barImage;

    private SoulManager _soulManager;

    private void Start()
    {
        barImage = GameObject.FindGameObjectWithTag("SoulBar").GetComponent<Image>();
        _soulManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulManager>();
        float currentSoul = _soulManager.GetCurrentSoul();
        float maxSoul = _soulManager.GetMaxSoul();
        
        Debug.Log(currentSoul / maxSoul);
        barImage.fillAmount = currentSoul / maxSoul;

    }
    // Returns a bool stating whether or not it was able to add the full amount
    public bool AddSoul(float amount)
    {
        if (amount + currentSoul > maxSoul)
        {
            currentSoul = maxSoul;
            barImage.fillAmount = currentSoul/maxSoul;
            return false;
        }
        
        currentSoul += amount;
        barImage.fillAmount = currentSoul / maxSoul;
        return true;
    }

    //what I am trying to do: get game object SoulObject, create a function that triggers the addsoul thing on contact with soul object



    // Returns a bool stating weather or not it can subtract the soul. (Doesn't subtract the soul if there isn't enough cause then cant do ability)
    public bool SubtractSoul(float amount)
    {
        if (currentSoul - amount < minSoul)
        {
            return false;
        }

        currentSoul -= amount;
        barImage.fillAmount = currentSoul / maxSoul;
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

