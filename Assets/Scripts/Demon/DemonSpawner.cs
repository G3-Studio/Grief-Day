using JetBrains.Annotations;
using UnityEngine;

public class DemonSpawner : MonoBehaviour {

    [CanBeNull] private GameObject demon;
    
    void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    
    private void GameManagerOnGameStateChanged(GameState state) {
        if(state == GameState.Bargaining) {
            InvokeDemon();
        } else if (state == GameState.Depression) {
            Destroy(demon);
        }
    }

    private void InvokeDemon() {
        this.demon = Instantiate(Utils.Prefabs.DEMON);
        this.demon.transform.position = new Vector3(UnityEngine.Random.Range(-26, 13), Utils.Positions.yOffsets[UnityEngine.Random.Range(0, 4)] + 2.29f, 0);
        //5.416
    }
}
