using System;
using UnityEngine;

public class DoubleJump : SkillEffect
{
    protected override TimeSpan duration => new TimeSpan(-1);
    protected override TimeSpan cooldown => new TimeSpan(0);
    public override string skillName => "double_jump";

    public override bool isValid {
        get => !this.movement.Grounded;
    }
    
    public override bool canExecute {
        get => base.canExecute && !this.executing && !this.movement.Grounded && this.movement.canMove;
    }

    private readonly Player player;
    private readonly Movement movement;
    private bool executing = false;

    public DoubleJump(Player player) {
        this.player = player;
        this.movement = player.GetComponent<Movement>();
    }
    protected override void tickStart(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        this.executing = true;
        this.movement.ApplyJump();
    }

    protected override void tick(Player player, Rigidbody2D rigidBody, Vector2 movement) { }

    protected override void tickEnd(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        this.executing = false;
    }
}
