using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dashing")] 
    [SerializeField] private float dashSpeed = 50;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [HideInInspector] public bool isDashing;
    private float dashStartTime;
    private float dashCooldownStart;
    private Vector3 velocity;

    [Header("Input")] 
    [SerializeField] private KeyCode dashKey = KeyCode.E;

    [Header("References")] 
    [SerializeField] private CharacterController controller;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        CheckDash();
        DashMove();
    }

    private (float, float) GetHorizontalAndVerticalMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return (x, z); // Tuple return
    }

    private Vector3 GetDashDirection(float x, float z)
    {
        Transform myTransform = transform;
        Vector3 direction = myTransform.right * x + myTransform.forward * z; // This makes it so its moving locally so rotation is taken into consideration
        return direction;
    }

    private void CheckDash()
    {
        // if you are pressing dash key and its passed the cooldown
        if (Input.GetKeyDown(dashKey) && !isDashing && Time.time - dashStartTime > dashCooldown)
        {
            // No dashing if you are crouching or wall running
            if (_playerMovement.movementState == PlayerMovement.MovementState.Crouching ||
                _playerMovement.movementState == PlayerMovement.MovementState.WallRunning)
                return;
        
            (float x, float z) = GetHorizontalAndVerticalMovement(); // Tuple unpacking

            // If arent pressing a key (second needed command for dash) don't execute
            if (x == 0 && z == 0)
                return;
            
            StartDash(x, z);
        }
    }

    private void StartDash(float x, float z)
    {
        isDashing = true;
        _playerMovement.dashSpeed = dashSpeed;
        dashStartTime = Time.time;
        Vector3 direction = GetDashDirection(x, z);
        velocity = direction * dashSpeed;
    }

    private void DashMove()
    {
        if (isDashing)
        {
            // if its within the dash duration
            if (Time.time - dashStartTime < dashDuration)
            {
                controller.Move(velocity * Time.deltaTime);
            }
            else
            {
                isDashing = false;
            }
        }
    }
}
