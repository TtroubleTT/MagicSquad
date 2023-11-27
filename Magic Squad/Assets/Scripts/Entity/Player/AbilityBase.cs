using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    protected abstract float SoulCost { get; set; }

    private static SoulManager _soulManager;

    protected virtual void DoAbility()
    {
        // how to reference soul manager google
        Debug.Log("before bool");
        bool canDoAbility = _soulManager.SubtractSoul(SoulCost);
        Debug.Log("after bool");
        if (!canDoAbility)
            return;
        
        // other scripts will extend this base behavior
    }
}
