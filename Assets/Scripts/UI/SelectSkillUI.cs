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
        this.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
        this.gameObject.transform.GetChild(1).GetComponent<Image>().enabled = false;
        Debug.Log(this.gameObject.transform.GetChild(2));
        Debug.Log(this.gameObject.transform.GetChild(2).GetComponent<Image>());
        this.gameObject.transform.GetChild(2).GetComponent<Image>().sprite = Sprites.FromName(skill1?.name);
        this.gameObject.transform.GetChild(3).GetComponent<Image>().sprite = Sprites.FromName(skill2?.name);
    }

    public void Disable() {
        this.gameObject.SetActive(false);
    }

    // If skill is 0, it selects left skill ; if skill is 1, it selects right skill
    public void SetSelectedSkill(int skill) {
        this.gameObject.transform.GetChild(skill).gameObject.GetComponent<Image>().enabled = true;
        this.gameObject.transform.GetChild((skill + 1) % 2).gameObject.GetComponent<Image>().enabled = false;
    }
}
