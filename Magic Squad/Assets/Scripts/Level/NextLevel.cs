using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string sceneName;
    private EnemyKills _enemyKills;

    private void Start()
    {
        _enemyKills = GameObject.FindGameObjectWithTag("KillUI").GetComponent<EnemyKills>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _enemyKills.SaveData();
            SceneManager.LoadScene(sceneName);
        }
    }
}
