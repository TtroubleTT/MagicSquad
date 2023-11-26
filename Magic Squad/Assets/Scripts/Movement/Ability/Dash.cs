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

    private void Update()
    {
        DoDash();
        DashMove();
    }

    private void DoDash()
    {
        // if you are pressing dash key and its passed the cooldown
        if (Input.GetKeyDown(dashKey) && !isDashing && Time.time - dashStartTime > dashCooldown)
        {
            isDashing = true;
            dashStartTime = Time.time;
            velocity = transform.forward * dashSpeed;
        }
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
