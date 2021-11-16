using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    private float rawTimer = 0f;
    public int timerSeconds = 0;
    // Start is called before the first frame update
    void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        rawTimer += Time.deltaTime;  // the raw timer value, a complicated float

        int seconds = Convert.ToInt32(rawTimer % 60);
        int minutes = Convert.ToInt32(Math.Floor(rawTimer / 60) % 60);

        timerSeconds = seconds + minutes*60;
        
    }
}