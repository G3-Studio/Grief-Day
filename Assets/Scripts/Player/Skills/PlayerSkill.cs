using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class SkillEffect {
    protected abstract TimeSpan duration { get; }
    protected abstract TimeSpan cooldown { get; }
    protected abstract string skillName { get; }

    private DateTime? triggerdAt;
    private DateTime? effectStart;

    protected abstract void tickStart(Player player, Rigidbody2D rigidBody, Vector2 movement);
    protected abstract void tick(Player player, Rigidbody2D rigidBody, Vector2 movement);
    protected abstract void tickEnd(Player player, Rigidbody2D rigidBody, Vector2 movement);

    public bool isValid {
        get => this.triggerdAt != null && this.triggerdAt + this.duration > DateTime.Now;
    }

    public bool canExecute {
        get => this.effectStart == null || this.effectStart + this.cooldown < DateTime.Now;
    }

    public void execute(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        if (!this.canExecute /*|| (!player.inventory?.HasSkill(this.skillName) ?? false)*/) return;
        this.triggerdAt = this.effectStart = DateTime.Now;
        this.tickStart(player, rigidBody, movement);
    }

    public void update(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        if (this.triggerdAt == null) return;
        if (this.isValid) {
            this.tick(player, rigidBody, movement);
        } else {
            this.tickEnd(player, rigidBody, movement);
            this.triggerdAt = null;
        }
    }
}