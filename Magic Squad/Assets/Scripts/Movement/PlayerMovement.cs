using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerBody;
    
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 12f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private float crouchSpeed = 5f;
    private float _currentSpeed;
    
    [Header("Key binds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;
    
    [Header("Physics")]
    [SerializeField] private float gravity = - 9.81f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private float jumpForce;

    [Header("Crouching")] 
    [SerializeField] private float crouchYScale;
    private float _startYScale;

    [Header("WallRunning")] 
    public float wallRunSpeed;
    public bool wallRunning;
    
    // Input
    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDirection;
    private Rigidbody rb;

    [SerializeField] private float groundDrag;
    
    // Velocity of movement
    public Vector3 velocity;
    public bool useGravity = true;
    private bool _isGrounded;
    
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
    
    private void Start()
    {
        _startYScale = transform.localScale.y;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Gets Player Input
        PlayerInput();

        // Resets falling velocity if they are no longer falling
        ResetVelocity();
        
        // Limits speed
        SpeedControl();
        
        // Handles what movement state we are in
        MovementStateHandler();

        // Jumping
        CheckJump();
        
        // Crouching
        CheckCrouch();

        // Gravity
        Gravity();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // calculate movement direction
        _moveDirection = playerBody.forward * _verticalInput + playerBody.right * _horizontalInput;
        
        rb.AddForce(_moveDirection.normalized * (10 * walkSpeed), ForceMode.Force);
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
        
        // handle drag
        if (_isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        // Makes it so we arent changing velocity when on ground not falling
        /*
        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        */
    }

    private void SpeedControl()
    {
        Vector3 currentVel = rb.velocity;
        Vector3 flatVel = new Vector3(currentVel.x, 0f, currentVel.z);
        
        // limit velocity if needed
        if (flatVel.magnitude > walkSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * walkSpeed;
            rb.velocity = new Vector3(limitedVel.x, currentVel.y, limitedVel.z);
        }
    }

    private void CheckJump()
    {
        // Physics stuff for jumping
        if (Input.GetKey(jumpKey) && _isGrounded && movementState != MovementState.Crouching)
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
            // velocity.y += gravity * Time.deltaTime;
            // controller.Move(velocity * Time.deltaTime);
        }
    }
}
