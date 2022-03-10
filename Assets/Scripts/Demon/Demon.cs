using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Utils;

public class Demon : MonoBehaviour {

    public JsonUtils.CollectableItemJson.Skill[] skills { get; private set; } = new JsonUtils.CollectableItemJson.Skill[2];

    private void Awake() {
        for (int i = 0; i < 2; i++) {
            skills[i] = Inventory.SKILLS[UnityEngine.Random.Range(0, Inventory.SKILLS.Count)];
        }
    }

    public JsonUtils.CollectableItemJson.Skill SwitchSkill(int skillIndex, JsonUtils.CollectableItemJson.Skill newSkill) {
        JsonUtils.CollectableItemJson.Skill retValue = skills[skillIndex];
        skills[skillIndex] = newSkill;
        return retValue;
    }
}