using UnityEngine;
using UnityEngine.Events;
using System;

public class LifeController : MonoBehaviour
{

    public static event Action<GameObject> OnDamageTaken;
    public static event Action<GameObject> OnDied;

    [SerializeField] private int maxShields = 3;

    public int CurrentShields { get; private set; }
    private bool isAlive = true;


    public UnityEvent<int> OnShieldsChanged;

    private void Start()
    {
        CurrentShields = 0; 
        OnShieldsChanged?.Invoke(CurrentShields);
    }

    public void TakeDamage()
    {
        if (!isAlive) return;

        if (CurrentShields > 0)
        {
            CurrentShields--;
            OnShieldsChanged?.Invoke(CurrentShields);
            OnDamageTaken?.Invoke(gameObject); 
        }
        else
        {
            Die();
        }
    }

    public void AddShield()
    {
        if (!isAlive || CurrentShields >= maxShields) return;

        CurrentShields++;
        OnShieldsChanged?.Invoke(CurrentShields);
    }

    private void Die()
    {
        isAlive = false;
        OnDied?.Invoke(gameObject); 
    }
}