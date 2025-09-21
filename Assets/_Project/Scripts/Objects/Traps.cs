using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LifeController lifeController = other.GetComponent<LifeController>();
            if (lifeController != null)
            {
                lifeController.TakeDamage();
                
            }
        }
    }
}
