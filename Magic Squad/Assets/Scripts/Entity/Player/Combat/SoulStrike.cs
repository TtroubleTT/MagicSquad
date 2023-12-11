using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Close range attack
public class SoulStrike : MonoBehaviour, ICombat
{
    public float Damage { get; set; } = 50f;

    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform cam;

    [Header("Attack")]
    [SerializeField] private float attackDistance = 6f;
    [SerializeField] private float attackWidth = 2.5f;
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

    public void Attack()
    {
        Debug.Log("attack");
        bool hitEnemy = Physics.BoxCast(cam.position, new Vector3(attackWidth, attackWidth, attackWidth), cam.forward, out RaycastHit hitInfo, cam.rotation, attackDistance, enemyLayer);

        if (hitEnemy)
        {
            Debug.Log("hit");
            // If there is a better way to do this please tell me
            hitInfo.transform.gameObject.GetComponent<EnemyBase>().SubtractHealth(Damage);
        }
    }
}
