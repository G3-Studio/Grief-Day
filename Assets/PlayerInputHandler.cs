using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Movement mover; 
    private Stairs stairs;
    private SelectSkill selectSkill;
    private GameObject player;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        player = GameObject.FindGameObjectsWithTag("Player")[playerInput.playerIndex];

        mover = player.GetComponent<Movement>();
        stairs = player.GetComponent<Stairs>();
        selectSkill = player.GetComponent<SelectSkill>();
    }
    
    void OnMove(InputValue value)
    {
        mover.SetInputMoveVector(value.Get<Vector2>());
    }

    void OnJump(InputValue value)
    {
        mover.Jump();
    }

    void OnInteract(InputValue value)
    {
        stairs.Interact();
        selectSkill.Interact();
    }

    void OnSkill1(InputValue value) {
        mover.triggerSkill(0);
    }

    void OnSkill2(InputValue value) {
        mover.triggerSkill(1);
    }
}
