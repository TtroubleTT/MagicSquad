using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    
    [SerializeField] private AudioClip background;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip takingDamage;
    [SerializeField] private AudioClip gainSoul;
    [SerializeField] private AudioClip dash;
    [SerializeField] private AudioClip enemyDeath;
    [SerializeField] private AudioClip heal;
    [SerializeField] private AudioClip closeAttack;
    [SerializeField] private AudioClip projectile;

    public enum AudioType
    {
        BackGround,
        Death,
        TakingDamage,
        GainSoul,
        Dash,
        EnemyDeath,
        Heal,
        CloseAttack,
        Projectile,
    }

    private Dictionary<AudioType, AudioClip> audioStorage = new();

    private void InitializeAudio()
    {
        audioStorage.Add(AudioType.BackGround, background);
        audioStorage.Add(AudioType.Death, death);
        audioStorage.Add(AudioType.TakingDamage, takingDamage);
        audioStorage.Add(AudioType.GainSoul, gainSoul);
        audioStorage.Add(AudioType.Dash, dash);
        audioStorage.Add(AudioType.EnemyDeath, enemyDeath);
        audioStorage.Add(AudioType.Heal, heal);
        audioStorage.Add(AudioType.CloseAttack, closeAttack);
        audioStorage.Add(AudioType.Projectile, projectile);
    }

    private void Start()
    {
        InitializeAudio();
        musicSource.clip = background;
        musicSource.volume = 0.6f;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioType type)
    {
        AudioClip myClip = audioStorage[type];
        SFXSource.clip = myClip;
        SFXSource.volume = .4f;
        SFXSource.Play();
    }
}
