using UnityEngine;

[RequireComponent(typeof(PlayerAudio))]
public class DamageHandler : MonoBehaviour
{
    private PlayerAudio _playerAudio;

    private void Awake()
    {
        _playerAudio = GetComponent<PlayerAudio>();
    }

    private void OnEnable()
    {
        LifeController.OnDamageTaken += HandleDamage;
    }

    private void OnDisable()
    {
        LifeController.OnDamageTaken -= HandleDamage;
    }

    private void HandleDamage(GameObject damagedObject)
    {
        if (damagedObject == this.gameObject)
        {
            _playerAudio?.PlayDamageSound();
        }
    }
}