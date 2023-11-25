using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dashing")] 
    [SerializeField] private float dashForce;
    [SerializeField] private float dashUpwardForce;
    [SerializeField] private float dashDuration;

    [Header("Input")] 
    [SerializeField] private KeyCode dashKey = KeyCode.E;
    
    // Use what you did in wall running to dash
}
