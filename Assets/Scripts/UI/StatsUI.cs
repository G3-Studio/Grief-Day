using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{

    public void UpdateAll(int health, int maxHealth, float speed, float attack) {
        this.UpdatePV(health, maxHealth);
        this.UpdateSpeed(speed);
        this.UpdateAttack(attack);
    }

    public void UpdatePV(int health, int maxHealth) {
        this.gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = health.ToString();
    }
    
    public void UpdateAttack(float attack) {
        this.gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = attack.ToString();
    }

    public void UpdateSpeed(float speed) {
        this.gameObject.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = speed.ToString();
    }
    
}
