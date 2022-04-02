using UnityEngine;

public abstract class SkillEffect {
    protected abstract float duration { get; }
    protected abstract float cooldown { get; }
    public abstract string skillName { get; }
    public virtual bool hasBindings => true;

    private float triggerdAt = -1;
    private float effectStart = -1;

    protected abstract void TickStart(Player player);
    protected abstract void Tick(Player player);
    protected abstract void TickEnd(Player player);

    public virtual bool isValid {
        get => this.triggerdAt >= 0 && this.triggerdAt + this.duration > Time.time;
    }

    public virtual bool CanExecute(Player movement) {
        return this.effectStart < 0 || this.effectStart + this.cooldown < Time.time;
    }

    public void Execute(Player player) {
        if (!this.CanExecute(player) || (!player.inventory?.HasSkill(this.skillName) ?? false)) return;
        this.triggerdAt = this.effectStart = Time.time;
        this.TickStart(player);
    }

    public void Update(Player player) {
        if (this.triggerdAt < 0) return;
        if (this.isValid) {
            this.Tick(player);
        } else {
            this.TickEnd(player);
            this.triggerdAt = -1;
        }
    }
}