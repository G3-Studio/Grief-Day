using System;
using UnityEngine;

public class DoubleJump : SkillEffect
{
    protected override TimeSpan duration => new TimeSpan(-1);
    protected override TimeSpan cooldown => new TimeSpan(0);
    protected override string skillName => "double_jump";

    public new bool isValid {
        get => !this.canExecute;
    }
    
    public new bool canExecute {
        get => player.GetComponent<Movement>().Grounded && player.GetComponent<Movement>().canMove;
    }

    private readonly Player player;
    private readonly Movement movement;
    private readonly Rigidbody2D rb;

    public DoubleJump(Player player) {
        this.player = player;
        this.movement = player.GetComponent<Movement>();
        this.rb = player.GetComponent<Rigidbody2D>();
    }
    protected override void tickStart(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        this.movement.ApplyJump();
    }

    protected override void tick(Player player, Rigidbody2D rigidBody, Vector2 movement) { }

    protected override void tickEnd(Player player, Rigidbody2D rigidBody, Vector2 movement) { }
}
