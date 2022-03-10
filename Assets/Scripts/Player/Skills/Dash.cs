using System;
using UnityEngine;
using UnityEngine.InputSystem;

class DashSkill : SkillEffect
{
    protected static TimeSpan SKILL_DURATION = new TimeSpan(0, 0, 0, 0, 100);
    protected static TimeSpan SKILL_COOLDOWN = new TimeSpan(0, 0, 2);

    protected override string skillName => "dash";
    protected override TimeSpan duration => SKILL_DURATION;
    protected override TimeSpan cooldown => SKILL_COOLDOWN;

    private float initialGravityScale;
    private Vector2 initialVelocity;
    private Vector2 dashForce;

    protected override void tickStart(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        this.initialGravityScale = rigidBody.gravityScale;
        rigidBody.gravityScale = 0;
        this.initialVelocity = rigidBody.velocity;
        this.dashForce = movement.normalized * player.dashStrength * player.speed;

        // Do not collide with the ground or dash won't work
        rigidBody.transform.Translate(new Vector3(0, .01f, 0));
        rigidBody.velocity = this.dashForce;
    }

    protected override void tick(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        rigidBody.velocity = this.dashForce;
    }

    protected override void tickEnd(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        rigidBody.gravityScale = this.initialGravityScale;
        rigidBody.velocity = this.initialVelocity;
    }
}