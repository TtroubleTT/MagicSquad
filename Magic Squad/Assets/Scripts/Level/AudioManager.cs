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

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
