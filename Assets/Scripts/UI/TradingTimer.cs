using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TradingTimer : MonoBehaviour
{

    public static TradingTimer instance { get; private set; }
    private static float MAX_TRADING_TIME = 5;

    private bool activated;
    private float timer;

    public TradingTimer() {
        TradingTimer.instance = this;
    }

    void Update() {
        if (!this.activated) {
            return;
        }
        this.timer += Time.deltaTime;
        if (this.timer > MAX_TRADING_TIME) {
            this.activated = false;
            TradingManager.Stop();
            return;
        }
        TimeSpan timeLeft = TimeSpan.FromSeconds((int) (MAX_TRADING_TIME - this.timer));
        this.GetComponent<TextMeshProUGUI>().text = timeLeft.ToString(@"mm\:ss");
    }

    public void StartTimer() {
        activated = true;
        timer = 0;
    }

    public void StopTimer() {
        this.activated = false;
    }
}
