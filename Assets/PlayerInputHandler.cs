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
    private Player player;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        player = FindObjectsOfType<Player>().FirstOrDefault();

        var index = playerInput.playerIndex;
        mover = FindObjectsOfType<Movement>().FirstOrDefault();
        stairs = FindObjectsOfType<Stairs>().FirstOrDefault();
        selectSkill = FindObjectsOfType<SelectSkill>().FirstOrDefault();
    }
    
    void OnMove(InputValue value)
    {
        if(mover != null)
            mover.SetInputMoveVector(value.Get<Vector2>());
    }

    void OnJump(InputValue value)
    {
        mover.Jump();
    }

    public void OnInteract(InputValue value)
    {
        stairs.Interact();
        selectSkill.Interact();
    }
}
