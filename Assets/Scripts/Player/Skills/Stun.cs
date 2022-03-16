using System;

public class Stun : SkillEffect
{
    protected override TimeSpan duration => new TimeSpan(0, 0, 2);
    protected override TimeSpan cooldown => new TimeSpan(0, 0, 5);
    public override string skillName => "stun";

    private Movement target;

    public override bool isValid {
        get => base.isValid && this.target != null;
    }

    public override bool CanExecute(Player player) {
        return base.CanExecute(player) && player.reachablePlayers.Count > 0;
    }

    protected override void TickStart(Player player) {
        Player playerTarget = player.reachablePlayers[(new Random()).Next(player.reachablePlayers.Count - 1)];
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