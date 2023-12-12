using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKills : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private float _kills;

    private void Start()
    {
        LoadData();
    }

    public void AddKillAmount(float amount)
    {
        _kills += amount;
        text.SetText($"Kills: {_kills}");
    }

    public void SaveData()
    {
        Debug.Log(_kills);
        PlayerPrefs.SetFloat("Kills", _kills);
    }

    public void LoadData()
    {
        _kills = PlayerPrefs.GetFloat("Kills");
        text.SetText($"Kills: {_kills}");
    }
}
