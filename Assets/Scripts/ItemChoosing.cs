using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoosing : MonoBehaviour
{
    
    [SerializeField] GameObject itemSpawner;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;

    ItemSpawn timer;
    private int time;

    void Update()
    {
        time = TimeManager.Instance.timerSeconds;
        if (time == 90)
        {
            GameObject item = (GameObject)Instantiate(prefab, new Vector3((float)-15.63 , (float)4.37, -2), transform.rotation);
            GameObject item2 = (GameObject)Instantiate(prefab, new Vector3((float)-11.72, (float)4.37, -2), transform.rotation);
            GameObject item3 = (GameObject)Instantiate(prefab, new Vector3((float)-7.81,(float)4.37, -2), transform.rotation);
            player1.transform.position = new Vector3((float)-20, (float)4.37, player1.position.z);
            player2.transform.position = new Vector3((float)-5, (float)4.37, player1.position.z);
        }
    }
}
