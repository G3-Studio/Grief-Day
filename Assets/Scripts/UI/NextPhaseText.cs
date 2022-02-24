using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class NextPhaseText : MonoBehaviour
{
    private TextMeshProUGUI timerDisplay;
    private List<int> stateTimingList = new List<int>();


    private void Awake() {
        timerDisplay = gameObject.GetComponent<TextMeshProUGUI>();  // Define the timerDisplay object

        // Create a list containing State Change timing values, sorted
        foreach(int timing in GameManager.Instance.stateTimings.Keys) {
            stateTimingList.Add(timing);
        }
        stateTimingList.Sort();
    }

    // Update is called once per frame
    private void Update()
    {
        int index = -1;
        // If in Overview phase, the next phase timing is at stateTimingList[0], so set index at 0, and do the same for the others
        if (GameManager.Instance.State == GameState.Overview) {  
            index = 0;
        }
        if (GameManager.Instance.State == GameState.Deny) {  
            index = 1;
        }
        if (GameManager.Instance.State == GameState.Anger) {
            index = 2;
        }
        if (GameManager.Instance.State == GameState.Bargaining) {
            index = 3;
        }

        if (index == -1) {  // Only true in depression phase
            timerDisplay.text = "";
        } else {
            int timeUntilNextPhase = stateTimingList[index] - TimeManager.Instance.timerSeconds;
            timerDisplay.text = GameManager.Instance.stateOrder[index+1] + " in " + TimeSpan.FromSeconds(timeUntilNextPhase).ToString(@"mm\:ss");
        }
    }
}
