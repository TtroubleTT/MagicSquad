using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("WallRunning")] 
    
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallRunForce;
    [SerializeField] private float maxWallRunTime;
    private float _wallRunTimer;

    [Header("Input")] 
    
    private float _horizontalInput;
    private float _verticalInput;

    [Header("Detection")] 
    
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float minJumpHeight;
    private RaycastHit _leftWallHit;
    private RaycastHit _rightWallHit;
    private bool _wallLeft;
    private bool _wallRight;

    [Header("References")]
    
    [SerializeField] private Transform orientation;
    [SerializeField] private CharacterController controller;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        // Gets our player movement script
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        CheckForWall();
        WallRunningState();
    }

    private void FixedUpdate()
    {
        if (_playerMovement.wallRunning)
            WallRunningMovement();
    }

    private void CheckForWall()
    {
        Vector3 origin = transform.position;
        Vector3 direction = orientation.right;
        
        // Raycasts to check if their is a wall on right or left.
        _wallRight = Physics.Raycast(origin, direction, out _rightWallHit, wallCheckDistance, whatIsWall);
        _wallLeft = Physics.Raycast(origin, - direction, out _leftWallHit, wallCheckDistance, whatIsWall);
    }

    private bool IsInAir()
    {
        // Ray casts downwards a an amount to check if you are in the air. If the raycast hits nothing then you are above ground.
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void WallRunningState()
    {
        // Inputs
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if ((_wallLeft || _wallRight) && _verticalInput > 0 && IsInAir()) // If theres either a wall on left or right and we are in air and have upwards vertical input.
        {
            if (!_playerMovement.wallRunning)
                StartWallRun();
        }
        else
        {
            if(_playerMovement.wallRunning)
                StopWallRun();
        }
    }

    private void StartWallRun()
    {
        _playerMovement.wallRunning = true;
    }

    private void WallRunningMovement()
    {
        _playerMovement.useGravity = false;
        _playerMovement.velocity = new Vector3(_playerMovement.velocity.x, 0f, _playerMovement.velocity.z);

        Vector3 wallNormal = _wallRight ? _rightWallHit.normal : _leftWallHit.normal; // If its a right wall use the right walls vector if not use the left walls.
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        // Makes it so your forward direction is decided by where you are facing.
        if ((orientation.forward - wallForward).magnitude > (orientation.forward + wallForward).magnitude)
            wallForward = -wallForward;

        // forward force on wall
        controller.Move(wallForward * wallRunForce);

        // If having problem sticking to wall push to wall
    }

    private void StopWallRun()
    {
        _playerMovement.wallRunning = false;
        _playerMovement.useGravity = true;
    }
}
