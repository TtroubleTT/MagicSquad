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
    [SerializeField] private Transform cam;

    [Header("Attack")]
    [SerializeField] private float attackDistance = 5f;
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
        bool hitEnemy = Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, attackDistance, enemyLayer);

        if (hitEnemy)
        {
            hitInfo.transform.gameObject.GetComponent<EnemyBase>().SubtractHealth(Damage);
        }
    }
}
