using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    private int x = 0;
    private float y;
    private int j = 0;
    private int[] antiStacker = new int[4];
    private bool validatedX;
    float[] variables = new float[4] { -1.2f, 4.6f, 10.2f, 15.8f};

    void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        if(state == GameState.AngerCoins) {
            InvokeRepeating("SpawnCoins", 0, 20);
        } else {
            CancelInvoke("SpawnCoins");
            ClearCoins();
        }

    }
    void SpawnCoins()
    {
        for (int i =0; i<4; i++)
        {
            
            j = 0;
            Array.Clear(antiStacker, 0, antiStacker.Length);   
            while (j != 4)
            {
                validatedX = false;
                while(!validatedX) {
                    x = UnityEngine.Random.Range(-31, 9);
                    foreach(int prevPos in antiStacker) {
                        validatedX = (Math.Abs(prevPos - x) > 2);
                        if (!validatedX) break;
                    } // if validateX is false for one value, stays false, else become true
                }
                antiStacker[j] = x;

                y = variables[i];
                GameObject item = (GameObject)Instantiate(prefab, new Vector3(x, y, 0), transform.rotation, gameObject.transform);
                j++;
            }
        }
            
    }

    void ClearCoins() {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coins");
        foreach(GameObject coin in coins) {
            GameObject.Destroy(coin);
        }
    }
}
