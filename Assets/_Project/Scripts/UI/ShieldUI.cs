using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private LifeController lifeController;
    [SerializeField] private GameObject[] shieldIcons;

   

    public void UpdateUI(int currentShields)
    {
        for (int i = 0; i < shieldIcons.Length; i++)
        {
            shieldIcons[i].SetActive(i < currentShields);
        }
    }
}
