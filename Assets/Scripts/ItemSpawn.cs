using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    private int x = 0;
    private int y;
    private int j = 0;
    private int value;
    public int time = 0;
    int[] variables = new int[4] { -1, 5, 10, 16};
    void Spawner()
    {
        if (time == 0 | time == 30 | time == 60)
            {
                for (int i =0; i<4; i++)
                {
                    j = 0;      
                    while (j != 4)
                    {
                        value = x;
                        x = Random.Range(-31, 9);
                        if (x != value && x != value + 1 && x != value - 1)
                        {
                            y = variables[i];
                             GameObject item = (GameObject)Instantiate(prefab, new Vector3(x, y, 0), transform.rotation);
                            j++;
                        }
                        
                    }
                }
            }
    }
    void Start()
    {
        InvokeRepeating("Spawner", 0, 1);
    }
    void Update()
    {
        time = TimeManager.Instance.timerSeconds;
    }
}
