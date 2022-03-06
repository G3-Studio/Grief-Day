using Utils;
using System.Collections.Generic;
using UnityEngine;
public class Skills
{
    public static List<JsonUtils.CollectableItemJson.Skill> skills;
    public JsonUtils.CollectableItemJson.Skill skill;

    static Skills() {
        string json = System.IO.File.ReadAllText("Assets/Scenes/CollectableItems.json");
        skills = JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json).skill;
    } 
    public static JsonUtils.CollectableItemJson.Skill getSkill() 
    {
        JsonUtils.CollectableItemJson.Skill selectedSkill = skills[UnityEngine.Random.Range(0, skills.Count)];
        skills.Remove(selectedSkill);

        Debug.Log("Returned skill :" + selectedSkill.name);
        return selectedSkill;
    }

}
