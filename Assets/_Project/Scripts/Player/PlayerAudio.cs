using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _spinSound;
    [SerializeField] private AudioClip _landingSound;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _pickupSound;

    private AudioSource _audioSource;
    void Awake()
    {
       _audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayJumpSound()
    {
        _audioSource.pitch = Random.Range(1.2f, 1.4f);
        _audioSource.PlayOneShot(_jumpSound);
    }

    public void PlaySpinSound()
    {
        _audioSource.PlayOneShot(_spinSound);
    }
    public void PlayLandingSound()
    {
        _audioSource.PlayOneShot(_landingSound);
    }

    public void PlayDamageSound()
    {
        _audioSource.pitch = Random.Range(1.2f, 1.4f);
        _audioSource.PlayOneShot(_damageSound);
    }

    public void PlayDeathSound()
    {
        _audioSource.PlayOneShot(_deathSound);
    }
    public void PlayPickupSound()
    {
        _audioSource.PlayOneShot(_pickupSound);
    }
}
