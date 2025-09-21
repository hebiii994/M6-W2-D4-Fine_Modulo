using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeysUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keysText;
    public void UpdateKeysText(int collected, int total)
    {
        if (keysText != null)
        {
            keysText.text = $"Chiavi: {collected} / {total}";
        }
    }
}
