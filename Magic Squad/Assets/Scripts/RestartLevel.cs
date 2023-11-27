using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("respawning");
            Respawn();
        }
    }

    private void Respawn()
    {
        Debug.Log("loading scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}

