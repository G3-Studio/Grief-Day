using UnityEngine;
using TMPro;
using System;

public class CountText : MonoBehaviour
{
    [SerializeField] GameObject player;
    TextMeshProUGUI countDisplay;
    CollectableItems counterPlayer;
    private int countPlayer = 0;
    private void Awake() 
    {
        countDisplay = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        counterPlayer = player.GetComponent<CollectableItems>();
        countPlayer = counterPlayer.coinCount;
        countDisplay.text = countPlayer.ToString(); 
    }

}
