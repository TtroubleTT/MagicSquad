using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("WallRunning")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallRunSpeed = 0.3f;
    [SerializeField] private float wallRunClimbSpeed = 4;
    [SerializeField] private float wallJumpUpSpeed= 10;
    [SerializeField] private float wallJumpSideSpeed= 15;
    [SerializeField] private float wallJumpForwardSpeed = 10;
    [HideInInspector] public bool isWallJumping;
    [HideInInspector] public bool isWallRunning;
    private Vector3 velocity;

    [Header("Input")] 
    [SerializeField] private KeyCode upwardsRunKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode downwardsRunKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    private bool _upwardsRunning;
    private bool _downwardsRunning;
    private float _verticalInput;

    [Header("Detection")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float minJumpHeight;
    private RaycastHit _leftWallHit;
    private RaycastHit _rightWallHit;
    private bool _wallLeft;
    private bool _wallRight;

    [Header("References")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private CharacterController controller;
    private PlayerMovement _playerMovement;

    // Code has been inspired and modified a bit based on these tutorials
    // https://www.youtube.com/watch?v=gNt9wBOrQO4
    // https://www.youtube.com/watch?v=WfW0k5qENxM&t=72s
    
    private void Start()
    {
        // Gets our player movement script
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        CheckForWall();
        WallRunningState();
        WallJump();
        WallJumpMove();
    }

    private void FixedUpdate()
    {
        if (isWallRunning)
            WallRunningMovement();
    }

    private bool IsInAir()
    {
        // Ray casts downwards a an amount to check if you are in the air. If the raycast hits nothing then you are above ground.
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private Vector3 GetWallNormal()
    {
        return _wallRight ? _rightWallHit.normal : _leftWallHit.normal; // If its a right wall use the right walls vector if not use the left walls.
    }

    private Vector3 GetWallForward(Vector3 wallNormal)
    {
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);
        
        // Makes it so your forward direction is decided by where you are facing.
        if ((playerBody.forward - wallForward).magnitude > (playerBody.forward + wallForward).magnitude)
            wallForward = -wallForward;

        return wallForward;
    }
    
    private void CheckForWall()
    {
        Vector3 origin = transform.position;
        Vector3 direction = playerBody.right;
        
        // Raycasts to check if their is a wall on right or left.
        _wallRight = Physics.Raycast(origin, direction, out _rightWallHit, wallCheckDistance, whatIsWall);
        _wallLeft = Physics.Raycast(origin, - direction, out _leftWallHit, wallCheckDistance, whatIsWall);
    }

    private void WallRunningState()
    {
        // Inputs
        _verticalInput = Input.GetAxisRaw("Vertical");

        _upwardsRunning = Input.GetKey(upwardsRunKey);
        _downwardsRunning = Input.GetKey(downwardsRunKey);

        // Decides whether it needs to start or stop the wall run
        if ((_wallLeft || _wallRight) && _verticalInput > 0 && IsInAir()) // If theres either a wall on left or right and we are in air and have upwards vertical input.
        {
            if (!isWallRunning)
                StartWallRun();
        }
        else
        {
            if(isWallRunning)
                StopWallRun();
        }
    }

    private void StartWallRun()
    {
        isWallJumping = false;
        isWallRunning = true;
        _playerMovement.useGravity = false;
        _playerMovement.wallRunSpeed = wallRunSpeed;
        _playerMovement.velocity = new Vector3(_playerMovement.velocity.x, 0, _playerMovement.velocity.z); // So we don't build up a velocity
    }
    
    private void StopWallRun()
    {
        isWallRunning = false;
        _playerMovement.useGravity = true;
    }

    private void WallRunningMovement()
    {
        Vector3 wallNormal = GetWallNormal();
        Vector3 wallForward = GetWallForward(wallNormal);

        // forward force on wall
        controller.Move(wallForward * wallRunSpeed);
        
        // Upwards and downwards running
        UpAndDownWallRunning();

        // If having problem sticking to wall push to wall
    }

    private void UpAndDownWallRunning()
    {
        if (_upwardsRunning)
        {
            _playerMovement.velocity = new Vector3(_playerMovement.velocity.x, wallRunClimbSpeed, _playerMovement.velocity.z);
            controller.Move(_playerMovement.velocity * Time.deltaTime);
        }

        if (_downwardsRunning)
        {
            _playerMovement.velocity = new Vector3(_playerMovement.velocity.x, -wallRunClimbSpeed, _playerMovement.velocity.z);
            controller.Move(_playerMovement.velocity * Time.deltaTime);
        }
    }

    private void WallJump()
    {
        if (!Input.GetKeyDown(jumpKey) || !isWallRunning)
            return;

        isWallJumping = true;

        Vector3 wallNormal = GetWallNormal();
        Vector3 wallForward = GetWallForward(wallNormal);

        // Velocity
        velocity = transform.up * wallJumpUpSpeed + wallNormal * wallJumpSideSpeed; // adds side and up movement
        velocity += wallForward * wallJumpForwardSpeed; // adds forward movement
        _playerMovement.velocity = new Vector3(_playerMovement.velocity.x, 0, _playerMovement.velocity.z); // makes it so you don't just fall on the ground
    }

    private void WallJumpMove()
    {
        if (_playerMovement.IsGrounded())
        {
            isWallJumping = false;
            return;
        }

        if (isWallJumping)
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
