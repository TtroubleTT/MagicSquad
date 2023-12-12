using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip death;
    public AudioClip walking;
    public AudioClip takingDamage;
    public AudioClip gainSoul;
    public AudioClip dash;
    public AudioClip enemyDeath;
    public AudioClip heal;
    public AudioClip closeAttack;
    public AudioClip projectile;

    private void Start()
    {
        musicSource.clip = background;
        AudioSource audioSource = musicSource;
        audioSource.volume = 0.6f;
        musicSource.Play();
    }
}
