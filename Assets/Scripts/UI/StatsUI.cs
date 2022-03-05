using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{

    public void UpdateAll(int health, int maxHealth, float speed, float attack) {
        this.UpdatePV(health, maxHealth);
        this.UpdateSpeed(speed);
        this.UpdateAttack(attack);
    }

    public void UpdatePV(int health, int maxHealth) {
        this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PV : " + health + "/" + maxHealth;
    }
    
    public void UpdateSpeed(float speed) {
        this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Speed : " + speed;
    }
    
    public void UpdateAttack(float attack) {
        this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Attack : " + attack;
    }

}
