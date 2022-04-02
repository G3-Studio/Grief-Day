using UnityEngine;

public class DoubleJump : SkillEffect
{
    protected override float duration => 0f;
    protected override float cooldown => 0f;
    public override string skillName => "double_jump";

    public override bool isValid {
        get => !this.movement.Grounded;
    }
    
    public override bool CanExecute(Player player) {
        return base.CanExecute(player) && !this.executing && !this.movement.Grounded && this.movement.canMove;
    }
    private readonly Movement movement;
    private bool executing = false;

    public DoubleJump(Player player) {
        this.movement = player.GetComponent<Movement>();
    }
    protected override void TickStart(Player player) {
        this.executing = true;
        this.movement.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        this.movement.ApplyJump();
    }

    protected override void Tick(Player player) { }

    protected override void TickEnd(Player player) {
        this.executing = false;
    }
}