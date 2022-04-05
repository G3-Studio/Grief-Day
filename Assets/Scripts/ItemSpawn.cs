using UnityEngine;
using System;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject buffPrefab;
    [SerializeField] GameObject skillPrefab;
    float[] yOffsets = new float[4] { -0.39f, 7.43f, 15.96f, 23.64f };
    private bool hasSkillsSpawned = false;
    void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        if(state == GameState.Deny) {
            InvokeRepeating("SpawnBuffs", 0, 30);
        } else if (state == GameState.AngerCoins) {
            CancelInvoke("SpawnBuffs");
            ClearBuffs();
            ClearSkills();
            InvokeRepeating("SpawnCoins", 0, 20);
        } else {
            CancelInvoke("SpawnCoins");
            ClearCoins();
        }

    }
    void SpawnCoins()
    {
        int x = 0;
        float y;
        int j = 0;
        int[] antiStacker = new int[4];
        bool validatedX;

        for (int i =0; i<3; i++)
        {
            
            j = 0;
            Array.Clear(antiStacker, 0, antiStacker.Length);   
            while (j != 4)
            {
                validatedX = false;
                while(!validatedX) {
                    x = UnityEngine.Random.Range(-26, 13);
                    foreach(int prevPos in antiStacker) {
                        validatedX = (Math.Abs(prevPos - x) > 2);
                        if (!validatedX) break;
                    } // if validateX is false for one value, stays false, else become true
                }
                antiStacker[j] = x;

                y = yOffsets[i] + 0.15f;
                GameObject item = (GameObject)Instantiate(coinPrefab, new Vector3(x, y, 0), transform.rotation, gameObject.transform);
                j++;
            }
        }

        j = 0;
        Array.Clear(antiStacker, 0, antiStacker.Length);   
        while (j != 4)
        {
            validatedX = false;
            while(!validatedX) {
                x = UnityEngine.Random.Range(-22, 8);
                foreach(int prevPos in antiStacker) {
                    validatedX = (Math.Abs(prevPos - x) > 2);
                    if (!validatedX) break;
                } // if validateX is false for one value, stays false, else become true
            }
            antiStacker[j] = x;

            y = yOffsets[3] + 0.15f;
            GameObject item = (GameObject)Instantiate(coinPrefab, new Vector3(x, y, 0), transform.rotation, gameObject.transform);
            j++;
        }
            
    }
    void SpawnBuffs()
    {
        int x = 0;
        float y;
        int j = 0;
        int[] antiStacker = new int[2];
        bool validatedX;

        int time = TimeManager.Instance.timerSeconds;

        for (int i =0; i<3; i++)
        {
            j = 0;
            Array.Clear(antiStacker, 0, antiStacker.Length);   
            while (j != 2)
            {
                validatedX = false;
                while(!validatedX) {
                    x = UnityEngine.Random.Range(-26, 13);
                    foreach(int prevPos in antiStacker) {
                        validatedX = (Math.Abs(prevPos - x) > 2);
                        if (!validatedX) break;
                    } // if validateX is false for one value, stays false, else become true
                }
                antiStacker[j] = x;

                y = yOffsets[i];
                GameObject item = (GameObject)Instantiate(buffPrefab, new Vector3(x, y, 0), transform.rotation, gameObject.transform);
                j++;
            }

        }

        j = 0;
        Array.Clear(antiStacker, 0, antiStacker.Length);
        while (j != 2) {
            validatedX = false;
            while (!validatedX) {
                x = UnityEngine.Random.Range(-22, 8);
                foreach (int prevPos in antiStacker) {
                    validatedX = (Math.Abs(prevPos - x) > 2);
                    if (!validatedX) break;
                } // if validateX is false for one value, stays false, else become true
            }

            antiStacker[j] = x;

            y = yOffsets[3];
            GameObject item = (GameObject) Instantiate(buffPrefab, new Vector3(x, y, 0), transform.rotation,
                gameObject.transform);
            j++;
        }

        if (time >= 59 && hasSkillsSpawned == false) {
            hasSkillsSpawned = true;
            GameObject firstSkill = (GameObject)Instantiate(skillPrefab, new Vector3(-21, yOffsets[1], 0), transform.rotation, gameObject.transform);
            GameObject secondSkill = (GameObject)Instantiate(skillPrefab, new Vector3(7, yOffsets[1], 0), transform.rotation, gameObject.transform);
        }
            
    }
    void ClearCoins() {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coins");
        foreach(GameObject coin in coins) {
            GameObject.Destroy(coin);
        }
    }
    void ClearBuffs() {
        GameObject[] buffs = GameObject.FindGameObjectsWithTag("Buff");
        foreach(GameObject buff in buffs) {
            GameObject.Destroy(buff);
        }
    }
    void ClearSkills() {
        GameObject[] skills = GameObject.FindGameObjectsWithTag("Skill");
        foreach(GameObject skill in skills) {
            GameObject.Destroy(skill);
        }
    }

}
