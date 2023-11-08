using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;

    
    // Speed of player movement
    [SerializeField]
    private float speed = 12f;

    // How powerful gravity is
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

    // Update is called once per frame
    private void Update()
    {
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

        controller.Move(move * (speed * Time.deltaTime)); // Moving in the direction of move at the speed

        // Physics stuff for jumping
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity stuff
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }
}
