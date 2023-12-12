using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private EnemyKills _enemyKills;
    
    private void Start()
    {
        _enemyKills = GameObject.FindGameObjectWithTag("KillUI").GetComponent<EnemyKills>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        _enemyKills.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

