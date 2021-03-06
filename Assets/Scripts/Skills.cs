using Utils;
using System.Collections.Generic;
using UnityEngine;
public class Skills
{
    public static List<JsonUtils.CollectableItemJson.Skill> skills;
    public JsonUtils.CollectableItemJson.Skill skill;

    static Skills() {
        string json = Resources.Load<TextAsset>("JSON/CollectableItems").text;
        skills = JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json).skill;
    }
    public static JsonUtils.CollectableItemJson.Skill getSkill() 
    {
        JsonUtils.CollectableItemJson.Skill selectedSkill = skills[Random.Range(0, skills.Count)];
        skills.Remove(selectedSkill);

        return selectedSkill;
    }

}
