using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoosing : MonoBehaviour
{
    
    [SerializeField] GameObject itemSpawner;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;

    private Transform firstPlayer;
    private Transform secondPlayer;
    private int validatedChoices = 0;
    private GameObject shrine1, shrine2, shrine3;

    void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        if(state == GameState.AngerTrade) {
            phaseInstallation();
        } else if (state == GameState.Bargaining) {
            phaseSuppression();
        }
    }

    private void phaseInstallation() {
        shrine1 = (GameObject)Instantiate(prefab, new Vector3((float)-15.63 , (float)4.37, -2), transform.rotation);
        shrine2 = (GameObject)Instantiate(prefab, new Vector3((float)-11.72, (float)4.37, -2), transform.rotation);
        shrine3 = (GameObject)Instantiate(prefab, new Vector3((float)-7.81,(float)4.37, -2), transform.rotation);
        player1.transform.position = new Vector3((float)-20, (float)4.37, player1.position.z);
        player2.transform.position = new Vector3((float)-5, (float)4.37, player1.position.z);


        firstPlayer = (player1.GetComponent<CollectableItems>().coinCount > player2.GetComponent<CollectableItems>().coinCount) ? player1 : player2;
        secondPlayer = (player1.GetComponent<CollectableItems>().coinCount > player2.GetComponent<CollectableItems>().coinCount) ? player2 : player1;
        secondPlayer.GetComponent<Movement>().canMove = false;
    }

    private void phaseSuppression() {
        if (validatedChoices == 1) {
            chooseRandom();
        }
    }

    private void chooseRandom() {
        if (validatedChoices == 0) {
            GameObject[] shrineList = GameObject.FindGameObjectsWithTag("Shrine");
            GameObject selectedShrine = shrineList[Random.Range(0, shrineList.Length)];
            firstPlayer.GetComponent<Player>().inventory.AddSkill(selectedShrine.GetComponent<Shrine>().skill);
            Destroy(selectedShrine);
            skillSelected();
        } else if (validatedChoices == 1) {
            GameObject[] shrineList = GameObject.FindGameObjectsWithTag("Shrine");
            GameObject selectedShrine = shrineList[Random.Range(0, shrineList.Length)];
            secondPlayer.GetComponent<Player>().inventory.AddSkill(selectedShrine.GetComponent<Shrine>().skill);
            Destroy(selectedShrine);
            skillSelected();
        }
    }

    public void skillSelected() {
        if(validatedChoices == 0) {
            firstPlayer.GetComponent<Movement>().canMove = false;
            secondPlayer.GetComponent<Movement>().canMove = true;
            firstPlayer.transform.position = new Vector3((float)-20, (float)4.37, firstPlayer.position.z);
            validatedChoices += 1;
        } else if (validatedChoices == 1){
            firstPlayer.GetComponent<Movement>().canMove = true;
            foreach(GameObject shrine in GameObject.FindGameObjectsWithTag("Shrine")) {
                Destroy(shrine);
            }
            validatedChoices = 2;
        }
    }

    private void Update() {
        int timer = TimeManager.Instance.timerSeconds;
        if(timer == 5+90+70+10 && validatedChoices == 0) chooseRandom(); 
    }
}
