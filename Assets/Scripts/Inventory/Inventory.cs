using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using Utils;

public class Inventory
{

    private static JsonUtils.CollectableItemJson collectableItemJson = LoadItems();
    public static JsonUtils.CollectableItemJson.Buff[] BUFFS = collectableItemJson.buff;
    public static JsonUtils.CollectableItemJson.Skill[] SKILLS = collectableItemJson.skill;
    private static int buffSlotCount = 2;
    private static int skillSlotCount = 2;

    private static JsonUtils.CollectableItemJson LoadItems() {
        // Open JSON file
        string json = System.IO.File.ReadAllText("Assets/Scenes/CollectableItems.json");
        return JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json);
    }
    
    private JsonUtils.CollectableItemJson.Buff[] buffs;
    private JsonUtils.CollectableItemJson.Skill[] skills;
    private int buffCount;
    private int skillCount;

    public Inventory() {
        buffs = new JsonUtils.CollectableItemJson.Buff[2];
        skills = new JsonUtils.CollectableItemJson.Skill[2];
        buffCount = 0;
        skillCount = 0;
    }

    public bool isBuffInventoryFull() {
        return buffCount == 2;
    }

    public void AddBuff(JsonUtils.CollectableItemJson.Buff buff) {
        buffs[buffCount++] = buff;
    }

    public bool isSkillInventoryFull() {
        return skillCount == 2;
    }

    public void AddSkill(JsonUtils.CollectableItemJson.Skill skill) {
        skills[skillCount++] = skill;
    }

    public JsonUtils.CollectableItemJson.Buff GetBuffInSlot(int slot) {
        return buffs[slot];
    }

    public bool HasSkill(JsonUtils.CollectableItemJson.Skill searchedSkill) {
        foreach (JsonUtils.CollectableItemJson.Skill skill in skills) {
            if (skill == searchedSkill) {
                return true;
            }
        }
        return false;
    }

}