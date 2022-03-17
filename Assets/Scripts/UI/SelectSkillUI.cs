using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class SelectSkillUI : MonoBehaviour {
    
    public static SelectSkillUI instance { get; private set; }

    private SelectSkillUI() {
        SelectSkillUI.instance = this;
    }

    public void Enable(JsonUtils.CollectableItemJson.Skill skill1, JsonUtils.CollectableItemJson.Skill skill2) {
        this.gameObject.SetActive(true);
        this.EnableSide(0, skill1);
        this.EnableSide(1, skill2);
    }

    private void EnableSide(int index, JsonUtils.CollectableItemJson.Skill skill) {
        Transform child = this.transform.GetChild(index);
        child.GetChild(0).GetComponent<Image>().enabled = index == 0;
        if (skill != null) {
            child.GetChild(1).GetComponent<Image>().enabled = true;
            child.GetChild(1).GetComponent<Image>().sprite = Sprites.FromName(skill.name);
        } else {
            child.GetChild(1).GetComponent<Image>().enabled = false;
        }
    }

    public void Disable() {
        this.gameObject.SetActive(false);
    }

    // If skill is 0, it selects left skill ; if skill is 1, it selects right skill
    public void SetSelectedSkill(int skill) {
        this.transform.GetChild(skill).GetChild(0).gameObject.GetComponent<Image>().enabled = true;
        this.transform.GetChild((skill + 1) % 2).GetChild(0).gameObject.GetComponent<Image>().enabled = false;
    }
}
