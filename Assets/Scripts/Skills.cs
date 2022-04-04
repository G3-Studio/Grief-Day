using Utils;
using System.Collections.Generic;
using UnityEngine;
public class Skills
{
    private static List<JsonUtils.CollectableItemJson.Skill> allSkills;
    public static List<JsonUtils.CollectableItemJson.Skill> skills;
    public JsonUtils.CollectableItemJson.Skill skill;

    static Skills() {
        string json = Resources.Load<TextAsset>("JSON/CollectableItems").text;

        allSkills = JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json).skill;
        skills = new List<JsonUtils.CollectableItemJson.Skill>(allSkills);
    }
    public static JsonUtils.CollectableItemJson.Skill getSkill() 
    {
        JsonUtils.CollectableItemJson.Skill selectedSkill = skills[Random.Range(0, skills.Count)];
        skills.Remove(selectedSkill);

        // For the moment, we don't have enough skills, so to avoid crashes, we refill the list. That is NOT a long term solution
        if (skills.Count == 0) {
            skills = new List<JsonUtils.CollectableItemJson.Skill>(allSkills);
        }
        return selectedSkill;
    }

}
