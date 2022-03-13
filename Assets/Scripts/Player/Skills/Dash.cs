using System;
using UnityEngine;

class DashSkill : SkillEffect
{
    protected static readonly TimeSpan SKILL_DURATION = new TimeSpan(0, 0, 0, 0, 100);
    protected static readonly TimeSpan SKILL_COOLDOWN = new TimeSpan(0, 0, 2);

    public override string skillName => "dash";
    protected override TimeSpan duration => SKILL_DURATION;
    protected override TimeSpan cooldown => SKILL_COOLDOWN;

    private float initialGravityScale;
    private Vector2 initialVelocity;
    private Vector2 dashForce;

    public override bool CanExecute(Player player) {
        return base.CanExecute(player) && player.GetComponent<Movement>().canMove;
    }

    protected override void TickStart(Player player) {
        Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
        Movement movement = player.GetComponent<Movement>();
        this.initialGravityScale = rigidBody.gravityScale;
        rigidBody.gravityScale = 0;
        this.initialVelocity = rigidBody.velocity;
        this.dashForce = movement.axisInput.normalized * player.dashStrength * player.speed;

        // Do not collide with the ground or dash won't work
        rigidBody.transform.Translate(new Vector3(0, .01f, 0));
        rigidBody.velocity = this.dashForce;
    }

    protected override void Tick(Player player) {
        Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
        rigidBody.velocity = this.dashForce;
    }

    protected override void TickEnd(Player player) {
        Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = this.initialGravityScale;
        rigidBody.velocity = this.initialVelocity;
    }
}