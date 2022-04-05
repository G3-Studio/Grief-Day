using UnityEngine;
using TMPro;
using System;

public class GameTimerText : MonoBehaviour
{
    TextMeshProUGUI timerDisplay;
    private void Awake() {
        timerDisplay = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int timerSeconds = TimeManager.Instance.timerSeconds;  // get the timer in seconds from TimeManager
        TimeSpan time = TimeSpan.FromSeconds(timerSeconds);  // transform it into a TimeSpan

        timerDisplay.text = time.ToString(@"mm\:ss"); // format and display the timer
    }
}
