using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStrikeEffect : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1;
    private void Start()
    {
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
