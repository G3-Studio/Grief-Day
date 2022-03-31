using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Movement mover; 
    private Stairs stairs;
    private SelectSkill selectSkill;
    private GameObject player;
    private bool gamePaused = false;
    private DemonDetector demon;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        player = GameObject.FindGameObjectsWithTag("Player")[playerInput.playerIndex];

        mover = player.GetComponent<Movement>();
        stairs = player.GetComponent<Stairs>();
        selectSkill = player.GetComponent<SelectSkill>();
        demon = player.GetComponent<DemonDetector>();
    }
    
    void OnMove(InputValue value)
    {
        if (gamePaused) return;
        mover.SetInputMoveVector(value.Get<Vector2>());
    }

    void OnJump(InputValue value)
    {
        if (gamePaused) return;
        if (player.GetComponent<Player>().currentUI == CurrentUI.CHOOSE_DEMON_ITEM) {
            TradingManager.ConfirmChoice();
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            return;
        }
        mover.Jump();
    }

    void OnInteract(InputValue value)
    {
        if (gamePaused) return;
        stairs.Interact();
        selectSkill.Interact();
        demon.Interact();
    }

    void OnSkill1(InputValue value) {
        if (gamePaused) return;
        if (player.GetComponent<Player>().currentUI == CurrentUI.CHOOSE_DEMON_ITEM) return;
        mover.triggerSkill();
    }

    void OnSkill2(InputValue value) {
        if (gamePaused) return;
        if (player.GetComponent<Player>().currentUI == CurrentUI.CHOOSE_DEMON_ITEM) return;
        mover.triggerSkill();
    }

    void OnPause(InputValue value) {
        if (gamePaused) {
            TimeManager.Instance.Resume();
        } else {
            TimeManager.Instance.Pause();
        }
        gamePaused = !gamePaused;
    }
}
