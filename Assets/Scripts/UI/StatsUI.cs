using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{

    [SerializeField] public Gradient gradient;

    public void UpdateAll(int health, int maxHealth, float speed, float attack) {
        this.UpdatePV(health, maxHealth);
        this.UpdateSpeed(speed);
        this.UpdateAttack(attack);
    }

    public void UpdatePV(int health, int maxHealth) {
        Transform root = this.gameObject.transform.GetChild(0);
        Transform sliderTransformation = root.GetChild(1);
        Slider sliderComponent = sliderTransformation.GetComponent<Slider>();
        sliderComponent.maxValue = maxHealth;
        sliderComponent.value = health;
        sliderTransformation.GetComponent<Image>().color = gradient.Evaluate(sliderComponent.normalizedValue);
        root.GetChild(2).GetComponent<TextMeshProUGUI>().text = health + "/" + maxHealth;
    }
    
    public void UpdateSpeed(float speed) {
        this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Speed : " + speed;
    }
    
    public void UpdateAttack(float attack) {
        this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Attack : " + attack;
    }

}
