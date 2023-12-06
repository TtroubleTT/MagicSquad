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
        _soulManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulManager>();
        float currentSoul = _soulManager.GetCurrentSoul();
        float maxSoul = _soulManager.GetMaxSoul();

        barImage = GetComponent<Image>();
        Debug.Log(currentSoul/maxSoul);
        barImage.fillAmount = currentSoul/maxSoul;

    }

    public void SpendSoul(int amount) //spending a soul function, can be used for abilities
    {
        float currentSoul = _soulManager.GetCurrentSoul();
        float maxSoul = _soulManager.GetMaxSoul();

        if (currentSoul >= amount)
        {
            currentSoul -= amount;
            barImage.fillAmount = currentSoul/maxSoul;
        }
    }

    public void AddSoul(int amount) //gaining a soul from enemy drops and however we allow them to be picked up
    {
        float currentSoul = _soulManager.GetCurrentSoul();
        float maxSoul = _soulManager.GetMaxSoul();

        if (currentSoul <= amount)
        {
            currentSoul += amount;
            barImage.fillAmount = currentSoul/maxSoul;
        }
    }

}

