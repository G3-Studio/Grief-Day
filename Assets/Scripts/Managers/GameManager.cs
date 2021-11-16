using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public Dictionary<int, GameState> stateTimings = new Dictionary<int, GameState>()  // Create a dictionary with the State change timings
        {
            {5, GameState.Deny},
            {5+90, GameState.Anger},
            {5+90+30, GameState.Bargaining},
            {5+90+30+90, GameState.Depression},
        };
    public Dictionary<int, GameState> stateOrder = new Dictionary<int, GameState>()  // Create a dictionary with the State change timings
        {
            {0, GameState.Overview},
            {1, GameState.Deny},
            {2, GameState.Anger},
            {3, GameState.Bargaining},
            {4, GameState.Depression},
        };
    public static event Action<GameState> OnGameStateChanged;

    void Awake() {
        Instance = this;
    }

    void Start() {
        UpdateGameState(GameState.Overview);    
    }

    void Update() {
        UpdateState();  // State automatically changes according to the timing
    }

    public void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState)
        {   
            case GameState.Overview:
                break;
            case GameState.Deny:
                break;
            case GameState.Anger:
                break;
            case GameState.Bargaining:
                break;
            case GameState.Depression:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void UpdateState() {
        
        if(stateTimings.ContainsKey(TimeManager.Instance.timerSeconds)) {  // Check if the current timing is a State Change timing
            if(State != stateTimings[TimeManager.Instance.timerSeconds]) {  // If the State Change has already occured, block  (possible because UpdateState is called several times per seconds)
                
                UpdateGameState(stateTimings[TimeManager.Instance.timerSeconds]);  // Update the State

            }
        }
    }

}

public enum GameState {
    Overview,
    Deny,
    Anger,
    Bargaining,
    Depression
}