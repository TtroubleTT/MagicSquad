using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private float mouseXSensitivity = 100f;
    [SerializeField] private float mouseYSensitivity = 100f;

    [Header("References")]
    [SerializeField] private Transform orientation;

    // Rotations
    private float _xRotation;
    private float _yRotation;
    
    // Code has been inspired and modified a bit based on these tutorials
    // https://www.youtube.com/watch?v=f473C43s8nE&t=505s
    // https://www.youtube.com/watch?v=_QajrabyTJc
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseYSensitivity * Time.deltaTime;

        //Rotation based on mouse input
        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -90f, 90f); // Makes it so we can only look right above us and not flip our entire camera
        
        // rotate cam and body
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, _yRotation, 0); // rotates about the y axis aka left and right
    }
}
