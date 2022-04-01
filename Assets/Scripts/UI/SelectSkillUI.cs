using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class SelectSkillUI : MonoBehaviour {
    
    public static SelectSkillUI instance { get; private set; }

    private SelectSkillUI() {
        SelectSkillUI.instance = this;
    }

    public void Enable(bool isPlayer1, JsonUtils.CollectableItemJson.Skill skill1, JsonUtils.CollectableItemJson.Skill skill2) {
        this.gameObject.transform.localPosition = new Vector3(1280/4 * (isPlayer1 ? -1 : 1), 0, 0);  // 1280 is the size of the canvas
        this.gameObject.SetActive(true);
        this.EnableSide(0, skill1);
        this.EnableSide(1, skill2);
        String textContent = (TradingManager.GetTradingPhase() == 0 ? "Choisissez le skill que vous voulez" : "Choisissez avec quel skill vous voulez l'echanger");
        this.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = textContent;
        
    }

    private void EnableSide(int index, JsonUtils.CollectableItemJson.Skill skill) {
        Transform child = this.transform.GetChild(index);
        // By default, only enable left outline
        child.GetChild(2).GetComponent<Image>().enabled = index == 0;
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
        this.transform.GetChild(skill).GetChild(2).gameObject.GetComponent<Image>().enabled = true;
        this.transform.GetChild((skill + 1) % 2).GetChild(2).gameObject.GetComponent<Image>().enabled = false;
    }
}
