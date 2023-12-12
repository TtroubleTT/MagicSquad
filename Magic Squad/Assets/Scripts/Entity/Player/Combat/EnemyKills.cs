using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKills : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private float _kills;

    public void AddKillAmount(float amount)
    {
        _kills += amount;
        text.SetText($"Kills: {_kills}");
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("Kills", _kills);
    }
    
    
}
