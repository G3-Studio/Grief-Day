using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoosing : MonoBehaviour
{
    
    [SerializeField] GameObject itemSpawner;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;

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

    void phaseInstallation() {
        shrine1 = (GameObject)Instantiate(prefab, new Vector3((float)-15.63 , (float)4.37, -2), transform.rotation);
        shrine2 = (GameObject)Instantiate(prefab, new Vector3((float)-11.72, (float)4.37, -2), transform.rotation);
        shrine3 = (GameObject)Instantiate(prefab, new Vector3((float)-7.81,(float)4.37, -2), transform.rotation);
        player1.transform.position = new Vector3((float)-20, (float)4.37, player1.position.z);
        player2.transform.position = new Vector3((float)-5, (float)4.37, player1.position.z);

        player2.GetComponent<Movement>().canMove = false;
    }

    void phaseSuppression() {
        
    }

    public void skillSelected() {
        if(validatedChoices == 0) {
            player1.GetComponent<Movement>().canMove = false;
            player2.GetComponent<Movement>().canMove = true;
            player1.transform.position = new Vector3((float)-20, (float)4.37, player1.position.z);
            validatedChoices += 1;
        } else if (validatedChoices == 1){
            player1.GetComponent<Movement>().canMove = true;
            foreach(GameObject shrine in GameObject.FindGameObjectsWithTag("Shrine")) {
                Destroy(shrine);
            }
            validatedChoices += 1;
        }
    }
}
