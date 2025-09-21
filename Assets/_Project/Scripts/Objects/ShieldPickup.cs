using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LifeController lifeController = other.GetComponent<LifeController>();
            other.GetComponent<PlayerAudio>()?.PlayPickupSound();
            if (lifeController != null )
            {
                lifeController.AddShield();
                Destroy(gameObject);
            }
        }
    }
}
