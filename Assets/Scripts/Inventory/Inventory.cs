using System.Collections.Generic;
using Utils;
using UnityEngine;

public class Inventory
{
    private static JsonUtils.CollectableItemJson collectableItemJson = LoadItems();
    public static List<JsonUtils.CollectableItemJson.Buff> BUFFS = collectableItemJson.buff;
    public static List<JsonUtils.CollectableItemJson.Skill> SKILLS = collectableItemJson.skill;
    private static int skillSlotCount = 2;

    private static JsonUtils.CollectableItemJson LoadItems() {
        // Open JSON file
        string json = Resources.Load<TextAsset>("JSON/CollectableItems").text;
        return JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json);
    }
    
    private List<JsonUtils.CollectableItemJson.Buff> buffs;
    private JsonUtils.CollectableItemJson.Skill[] skills;
    private int buffCount;
    private int skillCount;

    public Inventory() {
        buffs = new List<JsonUtils.CollectableItemJson.Buff>();
        skills = new JsonUtils.CollectableItemJson.Skill[2];
        buffCount = 0;
        skillCount = 0;
    }

    public void AddBuff(JsonUtils.CollectableItemJson.Buff buff) {
        buffs.Add(buff);
    }
    
    public JsonUtils.CollectableItemJson.Buff GetBuffInSlot(int slot) {
        return slot < buffs.Count ? buffs[slot] : null;
    }

    public bool isSkillInventoryFull() {
        return skillCount == 2;
    }

    public void AddSkill(JsonUtils.CollectableItemJson.Skill skill) {
        skills[skillCount++] = skill;
    }

    public bool HasSkill(JsonUtils.CollectableItemJson.Skill searchedSkill) {
        foreach (JsonUtils.CollectableItemJson.Skill skill in skills) {
            if (skill == searchedSkill) {
                return true;
            }
        }
        return false;
    }

    public bool HasSkill(string skillName) {
        foreach (JsonUtils.CollectableItemJson.Skill skill in skills)
            if (skill?.name == skillName) return true;
        return false;
    }

    public JsonUtils.CollectableItemJson.Skill GetSkillInSlot(int slot) {
        return skills[slot];
    }
}
