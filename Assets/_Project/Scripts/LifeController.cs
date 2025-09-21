using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxShields = 3;
    [SerializeField] private float _deathAnimationDuration = 2f;
    [SerializeField] private Animator _animator;
    public int CurrentShields { get; private set; }
    private bool isAlive = true;
    private Rigidbody _rb;
    private PlayerAudio _playerAudio;

    public UnityEvent<int> OnShieldsChanged;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerAudio = GetComponent<PlayerAudio>();
    }
    void Start()
    {
        CurrentShields = 0;
        OnShieldsChanged?.Invoke(CurrentShields);
    }

    public void TakeDamage()
    {
        if (!isAlive) return;
        if (CurrentShields > 0)
        {
            _playerAudio?.PlayDamageSound();
            CurrentShields--;
            OnShieldsChanged?.Invoke(CurrentShields);
            Debug.Log($"Sei stato colpito, Scudi rimasti: {CurrentShields}");
        }
        else
        {
            Die();
        }
    }
    public void AddShield()
    {
        if (!isAlive) return;
        if (CurrentShields < maxShields)
        {
            CurrentShields++;
            OnShieldsChanged?.Invoke(CurrentShields);
            Debug.Log($"Boody GA o qualsiasi altra cosa dicesse UKA UKA, Scudi totali: {CurrentShields}");
            // se ho tempo cerco il suono di UKA UKA xD
        }
        else
        {
            //vedremo se farlo o meno più avanti

            Debug.Log("invincibilità?");
        }
    }

    private void Die()
    {
        isAlive = false;
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
            GetComponent<Collider>().isTrigger = true;
        }
            GetComponent<PlayerController>().enabled = false;
        _playerAudio?.PlayDeathSound();
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("IsDead");
        }
        yield return new WaitForSeconds(_deathAnimationDuration);
        GameManager.Instance.HandleGameOver();

    }
}
