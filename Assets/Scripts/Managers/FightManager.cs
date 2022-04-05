using UnityEngine;
using System;

public class FightManager : MonoBehaviour
{
    [SerializeField] GameObject player1, player2, frameMiddle, cameraP1, cameraP2;
    [SerializeField] Vector2 teleportPosP1, teleportPosP2;
    [SerializeField] int cameraSize;
    private bool cameraResizing = false;
    void Awake() {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state) {
        if(state == GameState.Depression) {
            player1.transform.position = new Vector3(teleportPosP1.x, teleportPosP1.y, player1.transform.position.z);
            player2.transform.position = new Vector3(teleportPosP2.x, teleportPosP2.y, player2.transform.position.z);

            frameMiddle.SetActive(false);
            cameraP2.SetActive(false);
            cameraP1.GetComponent<CameraController>().enabled = false;
            Camera camera = cameraP1.transform.GetChild(0).GetComponent<Camera>();
            camera.orthographicSize = cameraSize;
            camera.rect = new Rect(0f, 0f, 1f, 1f);
            camera.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, -100f);
            cameraResizing = true;
        }
    }

    private void Update() {
        if (cameraResizing) {
            Vector3 posP1 = player1.transform.position;
            Vector3 posP2 = player2.transform.position;
            Vector3 cameraPos1 = new Vector3(posP1.x, posP1.y+2, -100f);
            Vector3 cameraPos2 = new Vector3(posP2.x, posP2.y+2, -100f);
            cameraP1.transform.GetChild(0).transform.position = Vector3.Lerp(cameraPos1, cameraPos2, 0.5f);

            float size = (MathF.Abs(posP1.x - posP2.x))*0.5f;
            if (size > 8f) cameraP1.transform.GetChild(0).GetComponent<Camera>().orthographicSize = size;
        }
    }
}
