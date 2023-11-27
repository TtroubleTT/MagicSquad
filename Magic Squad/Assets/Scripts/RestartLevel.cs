using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if(collision.gameObject.CompareTag("Respawn"))
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

