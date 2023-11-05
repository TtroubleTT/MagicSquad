using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 100f;

    [SerializeField]
    private Transform playerBody;

    private float _xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Looking up and down
        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -90f, 90f); // Makes it so we can only look right above us and not flip our entire camera

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f); // Its rotating about the x axis aka up and down
        
        // Looking right and left
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
