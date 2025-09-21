using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CratesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cratesText;
    public void UpdateCratesText(int collected, int total)
    {
        if (cratesText != null)
        {
            cratesText.text = $"Casse: {collected} / {total}";
        }
    }
}
