using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    protected abstract float SoulCost { get; set; }

    private SoulManager _soulManager;

    private void Awake()
    {
        _soulManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulManager>();
    }

    protected abstract void InitializeAbstractedStats();

    protected virtual bool DoAbility()
    {
        bool canDoAbility = _soulManager.SubtractSoul(SoulCost);

        if (!canDoAbility)
            return false;

        return true;

        // other scripts will extend this base behavior
    }
}
