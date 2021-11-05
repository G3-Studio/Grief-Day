using UnityEngine;
using TMPro;
using System;

public class GameStateManager : MonoBehaviour
{
    float rawTimerValue;

    int timerInSeconds;
    [SerializeField] TextMeshProUGUI timerDisplay;
    [SerializeField] TextMeshProUGUI nextPhaseDisplay;
    [SerializeField] int overviewPhaseDuration = 5;
    [SerializeField] int denyPhaseDuration = 90;
    [SerializeField] int angerPhaseDuration = 30;
    [SerializeField] int bargainingPhaseDuration = 90;
    [SerializeField] int depressionPhaseDuration = 60;

    int overviewTiming;
    int denyTiming;
    int angerTiming;
    int bargainingTiming;
    int depressionTiming;
    int timeUntilNextPhase;

    string gamePhase; 
    string nextPhase;
    string[] phaseIndex = new string[5] {"overview", "deny", "anger", "bargaining", "depression"};


    void Start() {

        overviewTiming = 0;
        denyTiming = overviewTiming + overviewPhaseDuration;
        angerTiming = denyTiming + denyPhaseDuration;
        bargainingTiming = angerTiming + angerPhaseDuration;
        depressionTiming = bargainingTiming + bargainingPhaseDuration;

    }
    void Update()
    {
        UpdateTimer();
        ManageGameState();
    }

    void UpdateTimer()
    {
        // UPDATE THE RAW TIMER
        rawTimerValue += Time.deltaTime;


        // DISPLAY THE TIMER

        // build the displayed timer string ( can look better using TimeSpan and string.Format, but I didn't manage to make it work, so I used the 'math' way )
        string ss = Convert.ToInt32(rawTimerValue % 60).ToString("00");
        string mm = (Math.Floor(rawTimerValue / 60) % 60).ToString("00");
        string timerValue = (mm + ":" + ss);

        timerDisplay.text = timerValue; // display the string

        timerInSeconds = Int32.Parse(ss) + (Int32.Parse(mm)*60); // convert time in seconds, in order to use it later
    }

    void ManageGameState()
    {
        int index;
        if(timerInSeconds < denyTiming) {
            index = 0; // overview
            timeUntilNextPhase = denyTiming - timerInSeconds;

        } else if(timerInSeconds < angerTiming) {
            index = 1; // deny
            timeUntilNextPhase = angerTiming - timerInSeconds;
        } else if (timerInSeconds < bargainingTiming) {
            index = 2; // anger
            timeUntilNextPhase = bargainingTiming - timerInSeconds;

        } else if (timerInSeconds < depressionTiming) {
            index = 3; // bargaining
            timeUntilNextPhase = depressionTiming - timerInSeconds;

        } else {
            index = 4; // depression
        }

        gamePhase = phaseIndex[index];

        if (index != 4) {
            nextPhase = phaseIndex[index+1];
            nextPhaseDisplay.text = "Next phase :\n" + nextPhase + " in " + timeUntilNextPhase.ToString() + " seconds";
        } else {
            nextPhaseDisplay.text = "";
        }


        Debug.Log(timerInSeconds);
        Debug.Log(gamePhase);
    }

}
