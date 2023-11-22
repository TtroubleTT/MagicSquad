using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
        barImage.fillAmount = .2f;

        // This is how you get the script so you can reference items in the soul manager
        _soulManager = GetComponent<SoulManager>();
        float currentSouls = _soulManager.GetCurrentSoul(); // This is just an example this is how you can grab info from that other clas
    }
}

//so basically i am confused why i cannot reference the image of the bar, it gives lots of errors when I do
//also will i be able to reference the soul variables i made in the other script? or should i make dif ones