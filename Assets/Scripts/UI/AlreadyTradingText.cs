using UnityEngine;

public class AlreadyTradingText : MonoBehaviour
{
    public static AlreadyTradingText instance { get; private set; }
    private static float MAX_TRADING_TIME = 3;

    private bool activated;
    private float timer;

    public AlreadyTradingText() {
        AlreadyTradingText.instance = this;
    }

    void Update() {
        if (!this.activated) {
            return;
        }
        this.timer += Time.deltaTime;
        if (this.timer > MAX_TRADING_TIME) {
            this.activated = false;
            this.gameObject.SetActive(false);
            return;
        }
    }

    public void Display() {
        activated = true;
        timer = 0;
        this.gameObject.SetActive(true);
        this.transform.localPosition = new Vector3(1280/4 * (TradingManager.player.isPlayer1 ? 1 : -1), 69, 0);
    }
}
