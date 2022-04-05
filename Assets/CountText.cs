using UnityEngine;
using TMPro;

public class CountText : MonoBehaviour
{
    [SerializeField] GameObject player;
    TextMeshProUGUI countDisplay;
    CollectableItems counterPlayer;
    private int countPlayer = 0;
    private bool enabled = false;

    public CountText()
    {
        Debug.Log("on initialise !");
        GameManager.OnGameStateChanged += this.OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState) {
        Debug.Log("ça change, on a : " + gameState);
        Debug.Log(this);
        if (gameState == GameState.AngerCoins) {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(true);
        } else {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    
    private void Awake() 
    {
        countDisplay = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        counterPlayer = player.GetComponent<CollectableItems>();
        countPlayer = counterPlayer.coinCount;
        countDisplay.text = countPlayer.ToString(); 
    }

}
