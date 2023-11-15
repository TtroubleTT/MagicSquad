using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{
    [SerializeField]
    private float _maxSoul = 100;

    [SerializeField]
    private float _minSoul = 0;

    [SerializeField]
    private float _currentSoul = 0;

    public void AddSoul(float amount)
    {
        if (amount + _currentSoul > _maxSoul)
        {
            _currentSoul = _maxSoul;
        }
        else
        {
            _currentSoul = _currentSoul + amount;
        }
    }
}
