using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Close range attack
public class SoulStrike : MonoBehaviour, IDamage
{
    public float Damage { get; set; } = 50f;

    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attack")] 
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private float cooldown = 1f;
    private float _lastAttack;

    private void Update()
    {
        if (Input.GetKeyDown(attackKey) && Time.time - _lastAttack >= cooldown)
        {
            _lastAttack = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        // try raycasting cam instead
        Transform myTransform = transform;
        RaycastHit[] raycastHit = Physics.SphereCastAll(myTransform.position, attackRadius, myTransform.forward, attackDistance, enemyLayer);

        foreach (RaycastHit hit in raycastHit)
        {
            GameObject myObject = hit.transform.gameObject;
            if (myObject.CompareTag("Enemy"))
            {
                myObject.GetComponent<EnemyBase>().SubtractHealth(Damage);
            }
        }
    }
}
