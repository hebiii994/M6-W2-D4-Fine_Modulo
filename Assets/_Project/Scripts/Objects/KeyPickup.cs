using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAudio>()?.PlayPickupSound();
            GameManager.Instance.CollectKey();
            gameObject.SetActive(false);
        }
    }
}
