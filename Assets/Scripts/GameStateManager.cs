using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] int tradingPhaseDuration = 90;
    [SerializeField] int depressionPhaseDuration = 60;

    int overviewTiming;
    int denyTiming;
    int angerTiming;
    int tradingTiming;
    int depressionTiming;
    int timeUntilNextPhase;

    string gamePhase = "overview"; // map overview, with glowing items
    string nextPhase = "deny";


    void Start() {

        overviewTiming = 0;
        denyTiming = overviewTiming + overviewPhaseDuration;
        angerTiming = denyTiming + denyPhaseDuration;
        tradingTiming = angerTiming + angerPhaseDuration;
        depressionTiming = tradingTiming + tradingPhaseDuration;

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
        if(timerInSeconds < denyTiming) {
            timeUntilNextPhase = denyTiming - timerInSeconds;

        } else if(timerInSeconds >= denyTiming && timerInSeconds < angerTiming) {
            gamePhase = "deny";
            nextPhase = "anger";
            timeUntilNextPhase = angerTiming - timerInSeconds;

        } else if (timerInSeconds >= angerTiming && timerInSeconds < tradingTiming) {
            gamePhase = "anger";
            nextPhase = "trading";
            timeUntilNextPhase = tradingTiming - timerInSeconds;
        } else if (timerInSeconds >= tradingTiming && timerInSeconds < depressionTiming) {
            gamePhase = "trading";
            nextPhase = "depression";
            timeUntilNextPhase = depressionPhaseDuration - timerInSeconds;
        } else if (timerInSeconds >= depressionTiming) {
            gamePhase = "depression";
        }

        nextPhaseDisplay.text = "Next phase :\n" + nextPhase + " in " + timeUntilNextPhase.ToString() + " seconds";

        Debug.Log(timerInSeconds);
        Debug.Log(gamePhase);
    }

}
