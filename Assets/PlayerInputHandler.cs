using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Movement mover;
    private SkillManager skillManager;
    private Stairs stairs;
    private Attack attack;
    private SelectSkill selectSkill;
    private GameObject player;
    private bool gamePaused = false;
    private DemonDetector demon;
    private Player playerScript;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        player = GameObject.FindGameObjectsWithTag("Player")[playerInput.playerIndex];

        attack = player.GetComponent<Attack>();
        mover = player.GetComponent<Movement>();
        playerScript = player.GetComponent<Player>();
        skillManager = player.GetComponent<SkillManager>();
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
        skillManager.GetSkill<DoubleJump>().Execute(playerScript);
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

        skillManager.TriggerSkill(0);
    }

    void OnSkill2(InputValue value) {
        if (gamePaused) return;
        if (player.GetComponent<Player>().currentUI == CurrentUI.CHOOSE_DEMON_ITEM) return;
        
        skillManager.TriggerSkill(1);
    }

    void OnAttack(InputValue value) {
        if (gamePaused) return;

        attack.PerformAttack();
    }

    void OnBigAttack(InputValue value) {
        if (gamePaused) return;

        attack.PerformBigAttack();
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
