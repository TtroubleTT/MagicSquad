using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Close range attack
public class SoulStrike : MonoBehaviour, ICombat
{
    public float Damage { get; set; }

    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject soulStrikePrefab;

    [Header("Attack")] 
    [SerializeField] private float damage = 50f;
    [SerializeField] private float attackDistance = 6f;
    [SerializeField] private float attackWidth = 2.5f;
    [SerializeField] private KeyCode attackKey = KeyCode.Mouse0;
    [SerializeField] private float cooldown = 1f;
    private float _lastAttack;

    private AudioManager _audioManager;

    private void Start()
    {
        Damage = damage;
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

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
        _audioManager.PlaySoundEffect(AudioManager.AudioType.CloseAttack);
        Vector3 camPos = cam.position;
        Quaternion camRot = cam.rotation;
        Instantiate(soulStrikePrefab, camPos + cam.forward + (cam.right * 0.5f) - cam.up, camRot);
        bool hitEnemy = Physics.BoxCast(camPos, new Vector3(attackWidth, attackWidth, attackWidth), cam.forward, out RaycastHit hitInfo, camRot, attackDistance, enemyLayer);
        if (hitEnemy)
        {
            // If there is a better way to do this please tell me
            hitInfo.transform.gameObject.GetComponent<EnemyBase>().SubtractHealth(Damage);
        }
    }
}
