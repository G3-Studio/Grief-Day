using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Movement mover;
    private SkillManager skillManager;
    private Stairs stairs;
    private SelectSkill selectSkill;
    private GameObject player;
    private bool gamePaused = false;
    private Player playerScript;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        player = GameObject.FindGameObjectsWithTag("Player")[playerInput.playerIndex];

        mover = player.GetComponent<Movement>();
        playerScript = player.GetComponent<Player>();
        skillManager = player.GetComponent<SkillManager>();
        stairs = player.GetComponent<Stairs>();
        selectSkill = player.GetComponent<SelectSkill>();
    }
    
    void OnMove(InputValue value)
    {
        if (gamePaused) return;
        mover.SetInputMoveVector(value.Get<Vector2>());
    }

    void OnJump(InputValue value)
    {
        if (gamePaused) return;
        mover.Jump();
        skillManager.GetSkill<DoubleJump>().Execute(playerScript);
    }

    void OnInteract(InputValue value)
    {
        if (gamePaused) return;
        stairs.Interact();
        selectSkill.Interact();
    }

    void OnSkill1(InputValue value) {
        if (gamePaused) return;
        skillManager.TriggerSkill(0);
    }

    void OnSkill2(InputValue value) {
        if (gamePaused) return;
        skillManager.TriggerSkill(1);
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
