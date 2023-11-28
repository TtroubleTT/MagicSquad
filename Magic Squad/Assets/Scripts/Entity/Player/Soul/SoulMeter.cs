using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoulMeter : MonoBehaviour
{
    [SerializeField]
    private Image barImage;

    private SoulManager _soulManager;

    private void Start()
    {
        barImage = GetComponent<Image>();
        barImage.fillAmount = .5f;

        // This is how you get the script so you can reference items in the soul manager
        _soulManager = GetComponent<SoulManager>();
        float currentSoul = _soulManager.GetCurrentSoul(); // This is just an example this is how you can grab info from that other clas
    }

    public void SpendSoul(int amount) //spending a soul function, can be used for abilities
    {
        float currentSoul = _soulManager.GetCurrentSoul();

        if (currentSoul >= amount)
        {
            currentSoul -= amount;
            barImage.fillAmount = currentSoul;
        }
    }

    public void AddSoul(int amount) //gaining a soul from enemy drops and however we allow them to be picked up
    {
        float currentSoul = _soulManager.GetCurrentSoul();

        if (currentSoul <= amount)
        {
            currentSoul += amount;
            barImage.fillAmount = currentSoul;
        }
    }

}

