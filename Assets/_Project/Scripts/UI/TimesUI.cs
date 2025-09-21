using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public void UpdateTimerText(float timeInSeconds)
    {
        if (timerText == null) return;
        if (timeInSeconds < 0)
        {
            timeInSeconds = 0;
        }
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        timerText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }
}
