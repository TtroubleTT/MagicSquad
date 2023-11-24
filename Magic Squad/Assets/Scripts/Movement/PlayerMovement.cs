using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller;
    
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 12f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private float crouchSpeed = 5f;
    [HideInInspector] public float wallRunSpeed;
    private float _currentSpeed;
    
    [Header("Key binds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;
    private bool _isGrounded;
    
    [Header("Physics")]
    [SerializeField] private float gravity = - 9.81f;
    [HideInInspector] public bool useGravity = true;
    [HideInInspector] public Vector3 velocity;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 3f;

    [Header("Crouching")] 
    [SerializeField] private float crouchYScale;
    private float _startYScale;
    
    // Wall running
    [HideInInspector] public bool wallRunning = false;

    // Movement States
    public MovementState movementState;

    public enum MovementState
    {
        Walking,
        Sprinting,
        WallRunning,
        Crouching,
        Air,
    }
    
    // Code has been inspired and modified a bit based on these tutorials
    // https://www.youtube.com/watch?v=f473C43s8nE&t=505s
    // https://www.youtube.com/watch?v=_QajrabyTJc

    public bool IsGrounded()
    {
        return _isGrounded;
    }
    
    private void Start()
    {
        _startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // Handles what movement state we are in
        MovementStateHandler();
        
        // Resets falling velocity if they are no longer falling
        ResetVelocity();
        
        // Movement
        MoveInDirection();

        // Jumping
        CheckJump();
        
        // Crouching
        CheckCrouch();

        // Gravity
        Gravity();
    }

    private void MovementStateHandler()
    {
        // Determines the movement state and speed based on different conditions
        if (wallRunning)
        {
            movementState = MovementState.WallRunning;
            _currentSpeed = wallRunSpeed;
        }
        else if (Input.GetKey(crouchKey))
        {
            movementState = MovementState.Crouching;
            _currentSpeed = crouchSpeed;
        }
        else if (_isGrounded && Input.GetKey(sprintKey))
        {
            movementState = MovementState.Sprinting;
            _currentSpeed = sprintSpeed;
        }
        else if (_isGrounded)
        {
            movementState = MovementState.Walking;
            _currentSpeed = walkSpeed;
        }
        else
        {
            movementState = MovementState.Air;
        }
    }

    private void ResetVelocity()
    {
        // Sphere casts to check for ground
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Makes it so we arent changing velocity when on ground not falling
        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void MoveInDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Transform myTransform = transform;
        Vector3 move = myTransform.right * x + myTransform.forward * z; // This makes it so its moving locally so rotation is taken into consideration

        controller.Move(move * (_currentSpeed * Time.deltaTime)); // Moving in the direction of move at the speed
    }

    private void CheckJump()
    {
        // Physics stuff for jumping
        if (Input.GetKeyDown(jumpKey) && _isGrounded && movementState != MovementState.Crouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void CheckCrouch()
    {
        Vector3 localScale = transform.localScale;
        
        // If we push down the crouch key and we are crouching (not wall running) we decrease model size
        if (Input.GetKeyDown(crouchKey) && movementState == MovementState.Crouching)
        {
            transform.localScale = new Vector3(localScale.x, crouchYScale, localScale.z);
        }

        // When releasing crouch key sets our scale back to normal
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(localScale.x, _startYScale, localScale.z);
        }
    }

    private void Gravity()
    {
        // If we are currently using gravity this makes us fall
        if (useGravity)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
