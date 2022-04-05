using UnityEngine;

public class DestroyCoins : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    ItemSpawn timer;
    private int time;

    void Update()
    {
        time = TimeManager.Instance.timerSeconds;
        if (time == 90)
        {
            Destroy(this.gameObject);
        }    
    }
}
