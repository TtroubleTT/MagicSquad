using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    
    // Speed variables
    [SerializeField] 
    private float walkSpeed = 12f;

    [SerializeField] 
    private float sprintSpeed = 20f;
    
    private float _currentSpeed = 12f;
    
    // Key bind variables
    [SerializeField] 
    private KeyCode jumpKey = KeyCode.Space;

    [SerializeField] 
    private KeyCode sprintKey = KeyCode.LeftShift;

    // Gravity and Jumping Variables
    [SerializeField]
    private float gravity = - 9.81f;

    [SerializeField] 
    private float jumpHeight = 3f;

    [SerializeField] 
    private Transform groundCheck;

    [SerializeField] 
    private float groundDistance = 0.4f;

    [SerializeField] 
    public LayerMask groundMask;
    
    // Velocity of movement
    private Vector3 _velocity;
    
    private bool _isGrounded;
    
    // Current Movement State
    private MovementState _movementState;

    public enum MovementState
    {
        Walking,
        Sprinting,
        Air,
    }

    private void MovementStateHandler()
    {
        if (_isGrounded && Input.GetKey(sprintKey))
        {
            _movementState = MovementState.Sprinting;
            _currentSpeed = sprintSpeed;
        }
        else if (_isGrounded)
        {
            _movementState = MovementState.Walking;
            _currentSpeed = walkSpeed;
        }
        else
        {
            _movementState = MovementState.Air;
        }
    }
    
    private void Update()
    {
        // Handles what movement state we are in
        MovementStateHandler();
        
        // Resets falling velocity if they are no longer falling
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Transform myTransform = transform;
        Vector3 move = myTransform.right * x + myTransform.forward * z; // This makes it so its moving locally so rotation is taken into consideration

        controller.Move(move * (_currentSpeed * Time.deltaTime)); // Moving in the direction of move at the speed

        // Physics stuff for jumping
        if (Input.GetKey(jumpKey) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity stuff
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
