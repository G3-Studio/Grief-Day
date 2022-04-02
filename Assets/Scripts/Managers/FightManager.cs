using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FightManager : MonoBehaviour
{
    [SerializeField] GameObject player1, player2;
    [SerializeField] Vector2 teleportPosP1, teleportPosP2;
    void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        if(state == GameState.Depression) {
            player1.transform.position = new Vector3(teleportPosP1.x, teleportPosP1.y, player1.transform.position.z);
            player2.transform.position = new Vector3(teleportPosP2.x, teleportPosP2.y, player2.transform.position.z);
        }
    }
}
