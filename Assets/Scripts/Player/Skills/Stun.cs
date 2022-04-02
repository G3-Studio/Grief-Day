public class Stun : SkillEffect
{
    protected override float duration => 2f;
    protected override float cooldown => 5f;
    public override string skillName => "stun";

    private Movement target;

    public override bool isValid {
        get => base.isValid && this.target != null;
    }

    public override bool CanExecute(Player player) {
        return base.CanExecute(player) && player.getCollidingPlayer() != null;
    }

    protected override void TickStart(Player player) {
        Player playerTarget = player.getCollidingPlayer();
        // Not supposed to be null as CanExecute has been called before
        this.target = playerTarget.GetComponent<Movement>();
        this.target.canMove = false;
    }

    protected override void Tick(Player player) {
        this.target.canMove = false;
    }

    protected override void TickEnd(Player player) {
        this.target.canMove = true;
        this.target = null;
    }
}