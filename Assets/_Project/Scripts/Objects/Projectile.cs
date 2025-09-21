using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private float _lifetime = 5f;


    private void OnEnable()
    {
        Invoke(nameof(DisableProjectile), _lifetime);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<LifeController>()?.TakeDamage();
            DisableProjectile();
        }
        else if (!other.isTrigger) 
        {
            DisableProjectile();
        }
    }

    private void DisableProjectile()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
}
