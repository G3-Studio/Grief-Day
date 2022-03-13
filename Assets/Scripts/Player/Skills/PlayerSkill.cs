using System;

public abstract class SkillEffect {
    protected abstract TimeSpan duration { get; }
    protected abstract TimeSpan cooldown { get; }
    public abstract string skillName { get; }
    public virtual bool hasBindings => true;

    private DateTime? triggerdAt;
    private DateTime? effectStart;

    protected abstract void TickStart(Player player);
    protected abstract void Tick(Player player);
    protected abstract void TickEnd(Player player);

    public virtual bool isValid {
        get => this.triggerdAt != null && this.triggerdAt + this.duration > DateTime.Now;
    }

    public virtual bool CanExecute(Player movement) {
        return this.effectStart == null || this.effectStart + this.cooldown < DateTime.Now;
    }

    public void Execute(Player player) {
        if (!this.CanExecute(player) || (!player.inventory?.HasSkill(this.skillName) ?? false)) return;
        this.triggerdAt = this.effectStart = DateTime.Now;
        this.TickStart(player);
    }

    public void Update(Player player) {
        if (this.triggerdAt == null) return;
        if (this.isValid) {
            this.Tick(player);
        } else {
            this.TickEnd(player);
            this.triggerdAt = null;
        }
    }
}