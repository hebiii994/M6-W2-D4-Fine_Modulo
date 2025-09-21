using UnityEngine;
using System.Collections;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _deathAnimationDuration = 2f;

    private PlayerAudio _playerAudio;
    private Rigidbody _rb;
    private Collider _collider;
    private PlayerController _playerController;

    private void Awake()
    {

        _playerAudio = GetComponent<PlayerAudio>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        LifeController.OnDied += HandleDeath;
    }

    private void OnDisable()
    {
        LifeController.OnDied -= HandleDeath;
    }

    private void HandleDeath(GameObject deadObject)
    {
        if (deadObject == this.gameObject)
        {

            if (_playerController != null) _playerController.enabled = false;

            if (_rb != null)
            {
                _rb.velocity = Vector3.zero;
                _rb.useGravity = false;
            }

            if (_collider != null) _collider.isTrigger = true;

            _playerAudio?.PlayDeathSound();

            StartCoroutine(DeathSequence());
        }
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