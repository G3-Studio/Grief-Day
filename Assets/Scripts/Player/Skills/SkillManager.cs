using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private List<SkillEffect> skills = new List<SkillEffect>();
    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void OnEnable() {
        // Register skills
        skills.Add(new DashSkill());
        skills.Add(new DoubleJump(this.player));
    }

    private void LateUpdate() {
        // Execute skills after the end of the movement update as it may update values
        foreach (SkillEffect skill in this.skills) skill.Update(player);
    }
    
    /**
     * <summary>
     * <para>Triggers a skill from the skill keybinds.</para>
     * <para>
     * If the triggered skill cannot be activated from these keybindings,
     * skill will NOT be activated (eg. double jump)
     * </para>
     * See <see cref="SkillEffect.hasBindings"/> to check whether
     * a given skill can be triggered by this method
     * </summary>
     */
    public void TriggerSkill(int slot) {
        string skillName = this.player.inventory.GetSkillInSlot(slot % Inventory.skillSlotCount)?.name;
        if (skillName == null) return;
        SkillEffect skill = GetSkill(skillName);
        if (skill?.hasBindings == true) skill.Execute(player);
    }

    /**
     * <summary>Retrieves a skill from its type</summary>
     */
    #nullable enable
    public T? GetSkill<T>() where T : SkillEffect {
        foreach (SkillEffect skillEffect in this.skills)
            if (skillEffect is T) return (T) skillEffect;
        Debug.LogError("No such skill '" + nameof(T) + "' in SkillManager");
        return null;
    }

    #nullable enable
    public SkillEffect? GetSkill(string name) {
        foreach (SkillEffect skillEffect in this.skills)
            if (skillEffect?.skillName == name) return skillEffect;
        Debug.LogError("No such skill '" + name + "' in SkillManager");
        return null;
    }
}
