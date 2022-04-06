using System;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public Player player1;
    [SerializeField] public Player player2;

    [SerializeField] public GameObject player2Object;
    [SerializeField] public GameObject playersCoins;
    [SerializeField] public GameObject healthBars;

    public GameState State { get; private set; }
    public readonly Dictionary<int, GameState> stateTimings = new Dictionary<int, GameState>  // Create a dictionary with the State change timings
        {
            {5, GameState.Deny},
            {5+90, GameState.AngerCoins},
            {5+90+70, GameState.AngerTrade},
            {5+90+70+20, GameState.Bargaining},
            {5+90+70+20+90, GameState.Depression},
        };
    public readonly Dictionary<int, GameState> stateOrder = new Dictionary<int, GameState>  // Create a dictionary with the State change timings
        {
            {0, GameState.Overview},
            {1, GameState.Deny},
            {2, GameState.AngerCoins},
            {3, GameState.AngerTrade},
            {4, GameState.Bargaining},
            {5, GameState.Depression},
        };
    public static event Action<GameState> OnGameStateChanged;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        UpdateGameState(GameState.Overview);    
    }

    private void Update() {
        UpdateState();  // State automatically changes according to the timing
    }

    private void UpdateGameState(GameState newState) {
        State = newState;

        switch (newState)
        {   
            // TODO: Remove that and make it better
            case GameState.Overview:
                break;
            case GameState.Deny:
                break;
            case GameState.AngerCoins:
                playersCoins.SetActive(true);
                break;
            case GameState.AngerTrade:
                playersCoins.SetActive(false);
                break;
            case GameState.Bargaining:
                break;
            case GameState.Depression:
                healthBars.SetActive(true);
                player2Object.layer = LayerMask.NameToLayer("Player");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void UpdateState() {
        
        if(stateTimings.ContainsKey(TimeManager.Instance.timerSeconds)) {  // Check if the current timing is a State Change timing
            if(State < stateTimings[TimeManager.Instance.timerSeconds]) {  // If the State Change has already occured, block  (possible because UpdateState is called several times per seconds)
                
                UpdateGameState(stateTimings[TimeManager.Instance.timerSeconds]);  // Update the State
                return;
            }
        }
        if (State != GameState.Depression && this.player1.hasAlreadyTraded && this.player2.hasAlreadyTraded && !TradingManager.isTrading) {
            UpdateGameState(GameState.Depression);
        }
    }

}

public enum GameState {
    Overview,
    Deny,
    AngerCoins,
    AngerTrade,
    Bargaining,
    Depression
}
